using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Portfolio.Domain.Models;
using Service.Liquidity.Reports.Database;

namespace Service.Liquidity.Reports.Jobs
{
    public class PortfolioTradeJob : IStartable
    {
        private readonly ILogger<PortfolioTradeJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioTradeJob(ISubscriber<IReadOnlyList<AssetPortfolioTrade>> subscriber,
            ILogger<PortfolioTradeJob> logger,
            DatabaseContextFactory contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            subscriber.Subscribe(HandleTrades);
        }

        private async ValueTask HandleTrades(IReadOnlyList<AssetPortfolioTrade> trades)
        {
            _logger.LogInformation($"PortfolioTradeJob handle {trades.Count} trades.");

            await using var ctx = _contextFactory.Create();
            await ctx.SaveTradesAsync(trades);
        }

        public void Start()
        {
        }
    }
}