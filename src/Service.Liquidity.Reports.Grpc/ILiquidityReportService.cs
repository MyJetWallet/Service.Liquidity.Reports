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
        Task<GrpcList<PositionPortfolio>> GetClosedPositions();

        [OperationContract]
        Task<GrpcList<PortfolioTrade>> GetTrades();

        [OperationContract]
        Task<GrpcList<PortfolioTrade>> GetPositionTrades(GetPositionTradesRequest request);
    }
}