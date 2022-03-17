using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Grpc.Models
{
    [DataContract]
    public class GetFeeShareSettlementHistoryResponse
    {
        [DataMember(Order = 1)] public bool Success { get; set; }
        [DataMember(Order = 3)] public string ErrorText { get; set; }
        [DataMember(Order = 4)] public List<PortfolioFeeShare> PortfolioFeeShares { get; set; }

    }
}