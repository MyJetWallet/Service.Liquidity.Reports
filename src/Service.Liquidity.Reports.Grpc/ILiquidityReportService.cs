using System.ServiceModel;
using System.Threading.Tasks;
using Service.Liquidity.Reports.Grpc.Models;

namespace Service.Liquidity.Reports.Grpc
{
    [ServiceContract]
    public interface ILiquidityReportService
    {
        [OperationContract]
        Task<GetAssetPortfolioTradesResponse> GetAssetPortfolioTradesAsync(GetAssetPortfolioTradesRequest request);

        [OperationContract]
        Task<GetChangeBalanceHistoryResponse> GetChangeBalanceHistoryAsync();
        
        [OperationContract]
        Task<GetManualSettlementHistoryResponse> GetManualSettlementHistoryAsync();
        
        [OperationContract]
        Task<GetFeeShareSettlementHistoryResponse> GetFeeShareSettlementHistoryAsync();
    }
}