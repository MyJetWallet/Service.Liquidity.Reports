using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Domain.ExternalMarketApi;
using MyJetWallet.Sdk.Service;
using MyJetWallet.Sdk.Service.Tools;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Domain.Models.Models;
using Service.Liquidity.Reports.Grpc.Models.Exchange;
using GetWithdrawalsHistoryRequest = MyJetWallet.Domain.ExternalMarketApi.Dto.GetWithdrawalsHistoryRequest;
using Withdrawal = MyJetWallet.Domain.ExternalMarketApi.Models.Withdrawal;

namespace Service.Liquidity.Reports.Jobs
{
    public class ExchangeHistoryBackgroundJob : IStartable
    {
        private const int TimerSpan60Sec = 60;
        private const int PageSize100 = 100;
        private const string StartFrom2021 = "2021-11-01";

        private readonly ILogger<ExchangeHistoryBackgroundJob> _logger;
        private readonly MyTaskTimer _operationsTimer;
        private readonly DatabaseContextFactory _contextFactory;
        private readonly IExternalExchangeManager _exchangeManager;
        private readonly IExternalMarket _externalMarket;


        public ExchangeHistoryBackgroundJob(
            ILogger<ExchangeHistoryBackgroundJob> logger,
            DatabaseContextFactory contextFactory,
            IExternalExchangeManager exchangeManager,
            IExternalMarket externalMarket)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            _exchangeManager = exchangeManager;
            _externalMarket = externalMarket;
            _operationsTimer = new MyTaskTimer(nameof(ExchangeHistoryBackgroundJob),
                TimeSpan.FromSeconds(TimerSpan60Sec), logger, Process);
        }

        public void Start()
        {
            _operationsTimer.Start();
        }

        public void Stop()
        {
            _operationsTimer.Stop();
        }

        private async Task Process()
        {
            try
            {
                var responseExchange = _exchangeManager
                    .GetExternalExchangeCollectionAsync();

                var exchangeNames = responseExchange.Result.ExchangeNames ?? new List<string>();
                foreach (var exchangeName in exchangeNames)
                {
                    var exchangeType = ToExchangeType(exchangeName);
                    if (exchangeType == ExchangeType.None)
                    {
                        _logger.LogWarning("Get unknown exchange {@name}", exchangeName);
                        continue;
                    }

                    var dateFrom = await GetLatestWithdrawal(exchangeType);
                    var dateTo = NextDay(dateFrom);
                    var current = DateTime.UtcNow;

                    do
                    { 
                        dateTo = NextDay(dateFrom);

                        var responseWithdrawalsHistory = _externalMarket.GetWithdrawalsHistoryAsync(
                            new GetWithdrawalsHistoryRequest
                            {
                                ExchangeName = exchangeName,
                                From = dateFrom,
                                To = dateTo
                            });

                        if (responseWithdrawalsHistory.Result.IsError)
                        {
                            _logger.LogWarning(
                                "Cannot get withdrawals {@name} date from {@dateFrom} to {@dateTo}. Message: {@message}",
                                exchangeName, dateFrom, dateTo, responseWithdrawalsHistory.Result.ErrorMessage);
                            dateFrom = dateTo;

                            continue;
                        }

                        var withdrawals = responseWithdrawalsHistory.Result?.Withdrawals ?? new List<Withdrawal>();
                        var withdrawalsDb = new List<Service.Liquidity.Reports.Domain.Models.Models.Withdrawal>();


                        foreach (var withdrawal in withdrawals)
                        {
                            try
                            {
                                var item = new Service.Liquidity.Reports.Domain.Models.Models.Withdrawal()
                                {
                                    Exchange = exchangeType,
                                    TxId = withdrawal.TxId,
                                    InternalId = withdrawal.Id,
                                    ExchangeAsset = withdrawal.Symbol, //TODO: ConvertToOurSymbol
                                    Asset = withdrawal.Symbol,
                                    Date = withdrawal.Date,
                                    Notes = withdrawal.Note,
                                    Fee = withdrawal.Fee,
                                    FeeInUsd = 0, //TODO: ConvertToUsd
                                    Volume = withdrawal.Amount
                                };

                                if (item.Date > dateTo)
                                    dateTo = item.Date;
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex,
                                    "Failed to Handle ExchangeHistoryBackgroundJob::Withdrawal {@withdrawal} {@operation} {@ex}",
                                    withdrawal.ToJson(), ex.Message, ex);
                            }
                        }

                        await using var ctx = _contextFactory.Create();
                        await ctx.SaveExchangeWithdrawalsHistoryAsync(withdrawalsDb);
                    } while (dateTo <= current);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Handle ExchangeHistoryBackgroundJob {@operation} {@ex}",
                    ex.Message, ex);
            }
        }

        private async Task<DateTime> GetLatestWithdrawal(ExchangeType exchangeType)
        {
            var dateFrom = DateTime.ParseExact(StartFrom2021, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            await using var ctx = _contextFactory.Create();
            var withdrawal = ctx.ExchangeWithdrawals
                .Where(e => e.Exchange == exchangeType)
                .OrderBy(e => e.Id)
                .LastOrDefaultAsync();

            dateFrom = withdrawal?.Result?.Date ?? dateFrom;

            return dateFrom;
        }

        private static DateTime NextDay(DateTime current)
        {
            DateTime next = current.AddDays(1);
            return new DateTime(next.Year, next.Month, next.Day, 0, 0, 0);
        }

        private ExchangeType ToExchangeType(string name)
        {
            if (string.Compare(name, "FTX", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return ExchangeType.Ftx;
            }

            if (string.Compare(name, "Binance", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return ExchangeType.Binance;
            }

            return ExchangeType.None;
        }
    }
}