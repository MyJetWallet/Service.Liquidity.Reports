using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Portfolio.Domain.Models;
using Service.Liquidity.Reports.Database;

namespace Service.Liquidity.Reports.Jobs
{
    public class PortfolioManualSettlementHistoryJob : IStartable
    {
        private readonly ILogger<PortfolioManualSettlementHistoryJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioManualSettlementHistoryJob(ISubscriber<IReadOnlyList<ManualSettlement>> subscriber,
            DatabaseContextFactory contextFactory,
            ILogger<PortfolioManualSettlementHistoryJob> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            subscriber.Subscribe(HandleChangeBalanceHistory);
        }

        private async ValueTask HandleChangeBalanceHistory(IReadOnlyList<ManualSettlement> settlements)
        {
            _logger.LogInformation($"PortfolioManualSettlementHistoryJob handle {settlements.Count} settlements.");
            await using var ctx = _contextFactory.Create();
            await ctx.SaveManualSettlementHistoryAsync(settlements);
        }

        public void Start()
        {
        }
    }
}