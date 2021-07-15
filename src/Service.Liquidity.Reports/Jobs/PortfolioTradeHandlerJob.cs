using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Portfolio.Domain.Models;
using Service.Liquidity.Reports.Database;

namespace Service.Liquidity.Reports.Jobs
{
    public class PortfolioTradeHandlerJob : IStartable
    {
        private ILogger<PortfolioTradeHandlerJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioTradeHandlerJob(ISubscriber<IReadOnlyList<AssetPortfolioTrade>> subscriber,
            ILogger<PortfolioTradeHandlerJob> logger,
            DatabaseContextFactory contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;
            subscriber.Subscribe(HandleTrades);
        }

        private async ValueTask HandleTrades(IReadOnlyList<AssetPortfolioTrade> trades)
        {
            await using var ctx = _contextFactory.Create();
            await ctx.SaveTradesAsync(trades);
        }

        public void Start()
        {
        }
    }
}