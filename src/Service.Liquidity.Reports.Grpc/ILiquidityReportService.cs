using System.ServiceModel;
using System.Threading.Tasks;
using Service.Liquidity.Engine.Domain.Models.Portfolio;
using Service.Liquidity.Engine.Grpc.Models;

namespace Service.Liquidity.Reports.Grpc
{
    [ServiceContract]
    public interface ILiquidityReportService
    {
        [OperationContract]
        Task<GrpcList<PositionPortfolio>> GetClosedPositions();

        [OperationContract]
        Task<GrpcList<PortfolioTrade>> GetTrades();
    }
}