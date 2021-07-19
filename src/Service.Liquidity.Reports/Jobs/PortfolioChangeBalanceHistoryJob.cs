using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Portfolio.Domain.Models;
using Service.Liquidity.Reports.Database;

namespace Service.Liquidity.Reports.Jobs
{
    public class PortfolioChangeBalanceHistoryJob : IStartable
    {
        private readonly ILogger<PortfolioTradeJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;
        
        public PortfolioChangeBalanceHistoryJob(ISubscriber<IReadOnlyList<ChangeBalanceHistory>> subscriber,
            DatabaseContextFactory contextFactory,
            ILogger<PortfolioTradeJob> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
            subscriber.Subscribe(HandleChangeBalanceHistory);
        }

        private async ValueTask HandleChangeBalanceHistory(IReadOnlyList<ChangeBalanceHistory> histories)
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