using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Service.Liquidity.Engine.Domain.Models.Portfolio;
using Service.Liquidity.Engine.Grpc.Models;
using Service.Liquidity.Reports.Database;
using Service.Liquidity.Reports.Grpc;
using Service.Liquidity.Reports.Grpc.Models;
using Service.Liquidity.Reports.Jobs;

namespace Service.Liquidity.Reports.Services
{
    public class LiquidityReportService : ILiquidityReportService
    {
        private readonly ReportAggregator _aggregator;
        private readonly DatabaseContextFactory _contextFactory;

        public LiquidityReportService(ReportAggregator aggregator, DatabaseContextFactory contextFactory)
        {
            _aggregator = aggregator;
            _contextFactory = contextFactory;
        }

        public async Task<GrpcList<PositionPortfolio>> GetClosedPositions(GetClosedPositionsRequest request)
        {
            await using var ctx = _contextFactory.Create();

            

            var entities =await ctx.Positions
                .Where(e => e.IsOpen == false && e.CloseTime < request.LastSeenDateTime)
                .OrderByDescending(e => e.CloseTime)
                .Take(request.Take ?? 30)
                .ToListAsync();

       
            

            var data = entities.Select(e => e.AsPositionPortfolio()).ToList();

            return GrpcList<PositionPortfolio>.Create(data);
        }

        public async Task<GrpcList<PortfolioTrade>> GetTrades(GetTradesRequest request)
        {
            await using var ctx = _contextFactory.Create();

            var entities = await ctx.PortfolioTrades
                .Where(e => e.DateTime < request.LastSeenDateTime)
                .OrderByDescending(e => e.DateTime)
                .Take(request.Take ?? 30)
                .ToListAsync();

            var data = entities.Select(e => e.AsPortfolioTrade()).ToList();

            return GrpcList<PortfolioTrade>.Create(data);
        }

        public async Task<GrpcList<PortfolioTrade>> GetPositionTrades(GetPositionTradesRequest request)
        {
            await using var ctx = _contextFactory.Create();

            var entities = await ctx.PositionAssociations
                .Include(e => e.Trade)
                .Where(e => e.PositionId == request.PositionId)
                .Select(e => e.Trade)
                .ToListAsync();

            var data = entities.Select(e => e.AsPortfolioTrade()).ToList();

            return GrpcList<PortfolioTrade>.Create(data);
        }
    }
}
