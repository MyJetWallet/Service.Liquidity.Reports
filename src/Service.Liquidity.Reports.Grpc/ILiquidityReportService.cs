using System;
using System.Runtime.Serialization;
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
    }

    [DataContract]
    public class GetClosedPositionsRequest
    {
        [DataMember(Order = 1)] public DateTime LastSeenDateTime { get; set; }

        [DataMember(Order = 2)] public int? Take { get; set; }
    }

    [DataContract]
    public class GetTradesRequest
    {
        [DataMember(Order = 1)] public DateTime LastSeenDateTime { get; set; }

        [DataMember(Order = 2)] public int? Take { get; set; }
    }
}