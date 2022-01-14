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
    public class PortfolioManualSettlementHistoryJob : IStartable
    {
        private readonly ILogger<PortfolioManualSettlementHistoryJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioManualSettlementHistoryJob(ISubscriber<IReadOnlyList<PortfolioSettlement>> subscriber,
            DatabaseContextFactory contextFactory,
            ILogger<PortfolioManualSettlementHistoryJob> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            subscriber.Subscribe(HandleChangeBalanceHistory);
        }

        private async ValueTask HandleChangeBalanceHistory(IReadOnlyList<PortfolioSettlement> portfolioSettlements)
        {
            _logger.LogInformation($"PortfolioManualSettlementHistoryJob handle {portfolioSettlements.Count} portfolioSettlements.");
            var settlements = portfolioSettlements;
            await using var ctx = _contextFactory.Create();
            await ctx.SaveManualSettlementHistoryAsync(settlements);
        }

        public void Start()
        {
        }
    }
}