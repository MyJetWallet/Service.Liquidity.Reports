using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Grpc;
using Service.Liquidity.Reports.Grpc.Models.Hedger;

namespace Service.Liquidity.Reports.Services
{
    public class HedgeReportService : IHedgeReportService
    {
        private readonly ILogger<HedgeReportService> _logger;
        private readonly DatabaseContextFactory _contextFactory;

        public HedgeReportService(
            ILogger<HedgeReportService> logger,
            DatabaseContextFactory contextFactory
        )
        {
            _logger = logger;
            _contextFactory = contextFactory;
        }

        public async Task<GetHedgeOperationsResponse> GetHedgeOperationsAsync(GetHedgeOperationsRequest request)
        {
            try
            {
                var lastDate = request.LastDate ?? DateTime.UtcNow;
                var batchSize = Math.Abs(request.BatchSize);

                await using var ctx = _contextFactory.Create();

                var records = ctx.HedgeOperations
                    .Where(o => o.CreatedDate < lastDate)
                    .OrderByDescending(o => o.CreatedDate)
                    .Take(batchSize)
                    .Include(o => o.HedgeTrades)
                    .AsNoTracking()
                    .ToList();

                return new GetHedgeOperationsResponse
                {
                    Items = records
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to GetHedgeOperations {@request}", request);
                return new GetHedgeOperationsResponse
                {
                    IsError = true,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}