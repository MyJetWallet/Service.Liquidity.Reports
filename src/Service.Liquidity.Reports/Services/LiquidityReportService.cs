using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
        private ILogger<LiquidityReportService> _logger;

        public LiquidityReportService(ReportAggregator aggregator,
            DatabaseContextFactory contextFactory,
            ILogger<LiquidityReportService> logger)
        {
            _aggregator = aggregator;
            _contextFactory = contextFactory;
            _logger = logger;
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

        public async Task<GetAssetPortfolioTradesResponse> GetAssetPortfolioTradesAsync(GetAssetPortfolioTradesRequest request)
        {
            var response = new GetAssetPortfolioTradesResponse();
            try
            {
                await using var ctx = _contextFactory.Create();
                var trades = await ctx.GetAssetPortfolioTrades(request.LastId, request.BatchSize, request.AssetFilter);

                long idForNextQuery = 0;
                if (trades.Any())
                {
                    idForNextQuery = trades.Select(elem => elem.Id).Min();
                }

                response.Success = true;
                response.Trades = trades;
                response.IdForNextQuery = idForNextQuery;
            } 
            catch (Exception exception)
            {
                _logger.LogError(JsonConvert.SerializeObject(exception));
                
                response.Success = false;
                response.ErrorMessage = exception.Message;
            }

            return response;
        }

        public async Task<GetChangeBalanceHistoryResponse> GetChangeBalanceHistoryAsync()
        {
            var response = new GetChangeBalanceHistoryResponse();
            try
            {
                await using var ctx = _contextFactory.Create();
                response.Histories = await ctx.GetChangeBalanceHistory();
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.ErrorText = exception.Message;
            }
            return response;
        }

        public async Task<GetManualSettlementHistoryResponse> GetManualSettlementHistoryAsync()
        {
            var response = new GetManualSettlementHistoryResponse();
            try
            {
                await using var ctx = _contextFactory.Create();
                response.Settlements = await ctx.GetManualSettlementHistory();
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.ErrorText = exception.Message;
            }
            return response;
        }
    }
}
