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
using MyJetWallet.Sdk.ServiceBus;
using Service.IndexPrices.Client;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Domain.Models.Models;
using Service.Liquidity.Reports.Grpc.Models.Exchange;
using GetWithdrawalsHistoryRequest = MyJetWallet.Domain.ExternalMarketApi.Dto.GetWithdrawalsHistoryRequest;
using WithdrawalApi = MyJetWallet.Domain.ExternalMarketApi.Models.Withdrawal;
using WithdrawalDb = Service.Liquidity.Reports.Domain.Models.Models.Withdrawal;


namespace Service.Liquidity.Reports.Jobs
{
    public class ExchangeHistoryBackgroundJob : IStartable
    {
        private const int TimerSpan60Sec = 60;
        private const int PageSize100 = 100;
        private const string StartFrom2022 = "2022-05-01";

        private readonly ILogger<ExchangeHistoryBackgroundJob> _logger;
        private readonly MyTaskTimer _operationsTimer;
        private readonly DatabaseContextFactory _contextFactory;
        private readonly IExternalExchangeManager _exchangeManager;
        private readonly IExternalMarket _externalMarket;
        private readonly IServiceBusPublisher<WithdrawalDb> _withdrawalHistoryPublisher;
        private readonly IIndexPricesClient _indexPricesClient;
        private readonly DateTime _minStartFrom;



        public ExchangeHistoryBackgroundJob(
            ILogger<ExchangeHistoryBackgroundJob> logger,
            DatabaseContextFactory contextFactory,
            IExternalExchangeManager exchangeManager,
            IExternalMarket externalMarket, 
            IServiceBusPublisher<WithdrawalDb> withdrawalHistoryPublisher, 
            IIndexPricesClient indexPricesClient)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            _exchangeManager = exchangeManager;
            _externalMarket = externalMarket;
            _withdrawalHistoryPublisher = withdrawalHistoryPublisher;
            _indexPricesClient = indexPricesClient;
            _operationsTimer = new MyTaskTimer(nameof(ExchangeHistoryBackgroundJob),
                TimeSpan.FromSeconds(TimerSpan60Sec), logger, Process);
            _minStartFrom = DateTime.ParseExact("2022-05-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
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
                    var latestWithdrawalOperation = await GetLatestWithdrawalDate(exchangeType);
                    var dateTo = latestWithdrawalOperation?.Date ?? _minStartFrom;
                    var current = DateTime.UtcNow;

                    do
                    { 
                        var dateFrom = dateTo;
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
                            
                            continue;
                        }

                        var withdrawals = responseWithdrawalsHistory.Result?.Withdrawals ?? new List<WithdrawalApi>();
                        var withdrawalsDb = new List<WithdrawalDb>();

                        foreach (var withdrawal in withdrawals)
                        {
                            try
                            {
                                var (assetIndexPrice, assetVolumeInUsd) =
                                    _indexPricesClient.GetIndexPriceByAssetVolumeAsync(withdrawal.Symbol,
                                        Convert.ToDecimal(withdrawal.Amount));

                                var (feeIndexPrice, feeVolumeInUsd) =
                                    _indexPricesClient.GetIndexPriceByAssetVolumeAsync(withdrawal.Symbol,
                                        Convert.ToDecimal(withdrawal.Fee));

                                
                                var item = new WithdrawalDb
                                {
                                    Exchange = exchangeType,
                                    TxId = withdrawal.TxId,
                                    InternalId = withdrawal.Id,
                                    ExchangeAsset = withdrawal.Symbol,
                                    Asset = withdrawal.Symbol, //TODO: ConvertToOurSymbol if needed
                                    Date = withdrawal.Date,
                                    Notes = withdrawal.Note,
                                    Fee = withdrawal.Fee,
                                    FeeInUsd = feeVolumeInUsd,
                                    Volume = withdrawal.Amount,
                                    VolumeInUsd = assetVolumeInUsd,
                                };
                                withdrawalsDb.Add(item);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex,
                                    "Failed to Handle ExchangeHistoryBackgroundJob::Withdrawal {@withdrawal} {@operation} {@ex}",
                                    withdrawal.ToJson(), ex.Message, ex);
                            }
                        }
                        if (withdrawalsDb.Count > 0)
                        {
                            await SaveToDbWithdrawals(withdrawalsDb);
                            await PublishWithdrawals(withdrawalsDb);
                        }
                    } while (dateTo <= current);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Handle ExchangeHistoryBackgroundJob {@operation} {@ex}",
                    ex.Message, ex);
            }
        }

        private async Task<Withdrawal> GetLatestWithdrawalDate(ExchangeType exchangeType)
        {
            try
            {
                await using var ctx = _contextFactory.Create();
                var withdrawal = await ctx.ExchangeWithdrawals
                    .Where(e => e.Exchange == exchangeType)
                    .OrderBy(e => e.Id)
                    .LastOrDefaultAsync();
                
                return withdrawal;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to GetLatestWithdrawalDate {@message} {@ex}",
                    ex.Message, ex);
            }
            return null;
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

            throw new ArgumentException("ExchangeType cannot be ", name);

        }

        private async Task PublishWithdrawals(List<WithdrawalDb> withdrawals)
        {
            foreach (var withdrawal in withdrawals)
            {
                await _withdrawalHistoryPublisher. PublishAsync(withdrawal); 
            }
        }
 
        private async Task SaveToDbWithdrawals(List<WithdrawalDb> withdrawals)
        {
            await using var ctx = _contextFactory.Create();
            await ctx.SaveExchangeWithdrawalsHistoryAsync(withdrawals);
        }
    }
}