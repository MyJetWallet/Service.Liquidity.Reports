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
    public class PortfolioFeeShareSettlementHistoryJob : IStartable
    {
        private readonly ILogger<PortfolioFeeShareSettlementHistoryJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioFeeShareSettlementHistoryJob(ISubscriber<IReadOnlyList<PortfolioFeeShare>> subscriber,
            DatabaseContextFactory contextFactory,
            ILogger<PortfolioFeeShareSettlementHistoryJob> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            subscriber.Subscribe(HandleChangeBalanceHistory);
        }

        private async ValueTask HandleChangeBalanceHistory(IReadOnlyList<PortfolioFeeShare> portfolioFeeShare)
        {
            _logger.LogInformation($"PortfolioFeeShareSettlementHistoryJob handle {portfolioFeeShare.Count} portfolioFeeShare.");
            var settlements = portfolioFeeShare;
            await using var ctx = _contextFactory.Create();
            await ctx.SaveFeeShareSettlementHistoryAsync(settlements);
        }

        public void Start()
        {
        }
    }
}