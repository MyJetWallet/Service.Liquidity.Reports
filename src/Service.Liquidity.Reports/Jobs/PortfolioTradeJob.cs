using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Jobs
{
    public class PortfolioTradeJob : IStartable
    {
        private readonly ILogger<PortfolioTradeJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioTradeJob(ISubscriber<IReadOnlyList<PortfolioTrade>> subscriber,
            ILogger<PortfolioTradeJob> logger,
            DatabaseContextFactory contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            subscriber.Subscribe(HandleTrades);
        }

        private async ValueTask HandleTrades(IReadOnlyList<PortfolioTrade> trades)
        {
            _logger.LogInformation($"PortfolioTradeJob handle {trades.Count} trades.");

            var count = trades.Count;
            trades = trades.GroupBy(e => e.TradeId).Select(e => e.First()).ToList();
            
            if (trades.Count != count)
                _logger.LogInformation($"PortfolioTradeJob handle {trades.Count} trades AFTER DEDUBLICATE.");
            
            await using var ctx = _contextFactory.Create();
            await ctx.SaveTradesAsync(trades);
        }

        public void Start()
        {
        }
    }
}