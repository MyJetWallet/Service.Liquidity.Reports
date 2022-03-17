using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Grpc.Models
{
    [DataContract]
    public class GetAssetPortfolioTradesResponse
    {
        [DataMember(Order = 1)] public bool Success { get; set; }
        [DataMember(Order = 2)] public string ErrorMessage { get; set; }
        [DataMember(Order = 3)] public long IdForNextQuery { get; set; }
        [DataMember(Order = 5)] public List<PortfolioTrade> PortfolioTrades { get; set; }
    }
}