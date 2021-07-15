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
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        
        public PortfolioTradeHandlerJob(ISubscriber<IReadOnlyList<AssetPortfolioTrade>> subscriber,
            ILogger<PortfolioTradeHandlerJob> logger,
            DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
        {
            _logger = logger;
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
            subscriber.Subscribe(HandleTrades);
        }

        private async ValueTask HandleTrades(IReadOnlyList<AssetPortfolioTrade> trades)
        {
            await using var ctx = DatabaseContext.Create(_dbContextOptionsBuilder);
            await ctx.SaveTradesAsync(trades);
        }

        public void Start()
        {
        }
    }
}