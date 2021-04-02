using System;
using System.Threading.Tasks;
using Service.Liquidity.Engine.Domain.Models.Portfolio;
using Service.Liquidity.Engine.Grpc.Models;
using Service.Liquidity.Reports.Grpc;
using Service.Liquidity.Reports.Jobs;

namespace Service.Liquidity.Reports.Services
{
    public class LiquidityReportService : ILiquidityReportService
    {
        private readonly ReportAggregator _aggregator;

        public LiquidityReportService(ReportAggregator aggregator)
        {
            _aggregator = aggregator;
        }

        public Task<GrpcList<PositionPortfolio>> GetClosedPositions()
        {
            var data = _aggregator.GetClosePositions();
            return Task.FromResult(GrpcList<PositionPortfolio>.Create(data));
        }

        public Task<GrpcList<PortfolioTrade>> GetTrades()
        {
            var data = _aggregator.GetTrades();
            return Task.FromResult(GrpcList<PortfolioTrade>.Create(data));
        }
    }
}
