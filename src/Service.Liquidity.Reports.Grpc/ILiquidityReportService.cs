using System.ServiceModel;
using System.Threading.Tasks;
using Service.Liquidity.Engine.Domain.Models.Portfolio;
using Service.Liquidity.Engine.Grpc.Models;
using Service.Liquidity.Reports.Grpc.Models;

namespace Service.Liquidity.Reports.Grpc
{
    [ServiceContract]
    public interface ILiquidityReportService
    {
        [OperationContract]
        Task<GrpcList<PositionPortfolio>> GetClosedPositions(GetClosedPositionsRequest request);

        [OperationContract]
        Task<GrpcList<PortfolioTrade>> GetTrades(GetTradesRequest request);

        [OperationContract]
        Task<GrpcList<PortfolioTrade>> GetPositionTrades(GetPositionTradesRequest request);
        
        [OperationContract]
        Task<GetAssetPortfolioTradesResponse> GetAssetPortfolioTradesAsync(GetAssetPortfolioTradesRequest request);
        
        [OperationContract]
        Task<GetPnlByTradeResponse> GetPnlByTradeAsync(GetPnlByTradeRequest request);
        
        [OperationContract]
        Task<GetChangeBalanceHistoryResponse> GetChangeBalanceHistoryAsync();
    }
}