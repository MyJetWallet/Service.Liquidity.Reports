using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Grpc.Core.Logging;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service;
using Newtonsoft.Json;
using Service.Liquidity.Engine.Domain.Models.Portfolio;
using Service.Liquidity.Reports.Database;

namespace Service.Liquidity.Reports.Jobs
{
    public class ReportAggregator
    {
        private readonly ILogger<ReportAggregator> _logger;
        private readonly DatabaseContextFactory _contextFactory;

        private readonly object _gate = new object();

        public ReportAggregator(
            ILogger<ReportAggregator> logger,
            ISubscriber<IReadOnlyList<PortfolioTrade>> tradeSubscriber,
            ISubscriber<IReadOnlyList<PositionAssociation>> associationPositionSubscriber,
            ISubscriber<IReadOnlyList<PositionPortfolio>> positionUpdateSubscriber,
            DatabaseContextFactory contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            tradeSubscriber.Subscribe(HandleTrades);
            associationPositionSubscriber.Subscribe(HandleAssociations);
            positionUpdateSubscriber.Subscribe(HandlePositionUpdate);
        }

        private async ValueTask HandlePositionUpdate(IReadOnlyList<PositionPortfolio> positions)
        {
            using var _ = MyTelemetry.StartActivity("Handle events ClosePosition")?.AddTag("event-count", positions.Count);

            var dict = new Dictionary<string, PositionPortfolio>();
            foreach (var position in positions)
            {
                dict[position.Id] = position;
            }

            var entities = dict.Values
                .Select(e => new PositionPortfolioEntity()
                {
                    WalletId = e.WalletId,
                    Symbol = e.Symbol,
                    QuotesAsset = e.QuotesAsset,
                    Side = e.Side,
                    BaseVolume = e.BaseVolume,
                    QuoteVolume = e.QuoteVolume,
                    IsOpen = e.IsOpen,
                    BaseAsset = e.BaseAsset,
                    CloseTime = e.CloseTime,
                    Id = e.Id,
                    OpenTime = e.OpenTime,
                    PLUsd = e.PLUsd,
                    QuoteAssetToUsdPrice = e.QuoteAssetToUsdPrice,
                    ResultPercentage = e.ResultPercentage,
                    TotalBaseVolume = e.TotalBaseVolume,
                    TotalQuoteVolume = e.TotalQuoteVolume
                });

            await using var ctx = _contextFactory.Create();

            await ctx.UpsetAsync(entities);

            _logger.LogInformation("Update {count} Position", positions.Count);
        }

        private async ValueTask HandleAssociations(IReadOnlyList<PositionAssociation> associations)
        {
            using var _ = MyTelemetry.StartActivity("Handle events Association")?.AddTag("event-count", associations.Count);

            var iteration = 0;
            while (true)
            {
                iteration++;
                try
                {
                    var entities = associations.Select(e => new PositionAssociationEntity()
                    {
                        Source = e.Source,
                        TradeId = e.TradeId,
                        PositionId = e.PositionId,
                        IsInternalTrade = e.IsInternalTrade
                    });

                    await using var ctx = _contextFactory.Create();

                    await ctx.UpsetAsync(entities);

                    _logger.LogInformation("Register {count} Association. Iterations: {iteration}", associations.Count, iteration);

                    return;
                }
                catch (Exception ex)
                {
                    if (iteration < 10)
                    {
                        await Task.Delay(2000);
                    }
                    else
                    {
                        _logger.LogError(ex, "Cannot handle PositionAssociation");
                        throw;
                    }
                }
            }
        }

        private async ValueTask HandleTrades(IReadOnlyList<PortfolioTrade> trades)
        {
            using var _ = MyTelemetry.StartActivity("Handle events Trade")?.AddTag("event-count", trades.Count);


            var entities = trades.Select(e => new PortfolioTradeEntity()
            {
                BaseVolume = e.BaseVolume,
                DateTime = e.DateTime,
                IsInternal = e.IsInternal,
                Price = e.Price,
                QuoteVolume = e.QuoteVolume,
                ReferenceId = e.ReferenceId,
                Side = e.Side,
                Source = e.Source,
                Symbol = e.Symbol,
                TradeId = e.TradeId
            });

            await using var ctx = _contextFactory.Create();

            await ctx.UpsetAsync(entities);

            _logger.LogInformation("Register {count} trades", trades.Count);
        }
    }
}