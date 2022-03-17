using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Jobs
{
    public class PortfolioChangeBalanceHistoryJob : IStartable
    {
        private readonly ILogger<PortfolioChangeBalanceHistoryJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioChangeBalanceHistoryJob(ISubscriber<IReadOnlyList<PortfolioChangeBalance>> subscriber,
            DatabaseContextFactory contextFactory,
            ILogger<PortfolioChangeBalanceHistoryJob> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            subscriber.Subscribe(HandleChangeBalanceHistory);
        }

        private async ValueTask HandleChangeBalanceHistory(IReadOnlyList<PortfolioChangeBalance> histories)
        {
            _logger.LogInformation($"PortfolioChangeBalanceHistoryJob handle {histories.Count} histories.");
            await using var ctx = _contextFactory.Create();
            await ctx.SaveChangeBalanceHistoryAsync(histories);
        }

        public void Start()
        {
        }
    }
}