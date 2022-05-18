using System.ServiceModel;
using System.Threading.Tasks;
using Service.Liquidity.Reports.Grpc.Models.Exchange;
using Service.Liquidity.Reports.Grpc.Models.Hedger;

namespace Service.Liquidity.Reports.Grpc;

[ServiceContract]
public interface IExchangeReportService
{
    [OperationContract]
    Task<GetWithdrawalsHistoryResponse> GetWithdrawalsHistoryAsync(GetWithdrawalsHistoryRequest request);
}