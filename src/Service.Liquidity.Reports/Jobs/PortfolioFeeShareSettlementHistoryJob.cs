using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Portfolio.Domain.Models;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Domain.Models;

namespace Service.Liquidity.Reports.Jobs
{
    public class PortfolioFeeShareSettlementHistoryJob : IStartable
    {
        private readonly ILogger<PortfolioFeeShareSettlementHistoryJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioFeeShareSettlementHistoryJob(ISubscriber<IReadOnlyList<FeeShareSettlement>> subscriber,
            DatabaseContextFactory contextFactory,
            ILogger<PortfolioFeeShareSettlementHistoryJob> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            subscriber.Subscribe(HandleChangeBalanceHistory);
        }

        private async ValueTask HandleChangeBalanceHistory(IReadOnlyList<FeeShareSettlement> settlements)
        {
            _logger.LogInformation($"PortfolioFeeShareSettlementHistoryJob handle {settlements.Count} settlements.");
            await using var ctx = _contextFactory.Create();
            await ctx.SaveFeeShareSettlementHistoryAsync(settlements);
        }

        public void Start()
        {
        }
    }
}