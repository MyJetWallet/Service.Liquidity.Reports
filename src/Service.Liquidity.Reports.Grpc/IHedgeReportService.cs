using System.ServiceModel;
using System.Threading.Tasks;
using Service.Liquidity.Reports.Grpc.Models.Hedger;

namespace Service.Liquidity.Reports.Grpc;

[ServiceContract]
public interface IHedgeReportService
{
    [OperationContract]
    Task<GetHedgeOperationsResponse> GetHedgeOperationsAsync(GetHedgeOperationsRequest request);
}