using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Grpc.Models
{
    [DataContract]
    public class GetManualSettlementHistoryResponse
    {
        [DataMember(Order = 1)] public bool Success { get; set; }
        [DataMember(Order = 2)] public List<PortfolioSettlement> Settlements { get; set; }
        [DataMember(Order = 3)] public string ErrorText { get; set; }
    }
}