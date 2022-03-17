using System;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Mapster;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Hedger.Domain.Models;
using Service.Liquidity.Reports.Database;

namespace Service.Liquidity.Reports.Jobs
{
    public class HedgeOperationJob : IStartable
    {
        private readonly ISubscriber<HedgeOperation> _subscriber;
        private readonly ILogger<PortfolioTradeJob> _logger;
        private readonly DatabaseContextFactory _contextFactory;

        public HedgeOperationJob(
            ISubscriber<HedgeOperation> subscriber,
            ILogger<PortfolioTradeJob> logger,
            DatabaseContextFactory contextFactory
        )
        {
            _subscriber = subscriber;
            _logger = logger;
            _contextFactory = contextFactory;
        }

        private async ValueTask Handle(HedgeOperation operation)
        {
            try
            {
                _logger.LogInformation($"Handle {nameof(HedgeOperation)} id: {operation.Id}");

                await using var ctx = _contextFactory.Create();

                ctx.HedgeOperations.Add(operation);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Handle {@operation}", operation);
            }
        }

        public void Start()
        {
            _subscriber.Subscribe(Handle);
        }
    }
}