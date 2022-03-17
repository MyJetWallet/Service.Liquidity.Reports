using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Mapster;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Hedger.Domain.Models;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Domain.Models;

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
            _logger.LogInformation($"Handle {nameof(HedgeOperationRecord)} id: {operation.Id}");

            await using var ctx = _contextFactory.Create();
            
            ctx.HedgeOperations.Add(operation.Adapt<HedgeOperationRecord>());
            //ctx.HedgeTrades.AddRange(operation.HedgeTrades.Adapt<IEnumerable<HedgeTradeRecord>>());

            await ctx.SaveChangesAsync();
        }

        public void Start()
        {
            _subscriber.Subscribe(Handle);
        }
    }
}