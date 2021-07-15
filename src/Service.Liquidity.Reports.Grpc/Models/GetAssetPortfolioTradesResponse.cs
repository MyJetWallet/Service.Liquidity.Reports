using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Reports.Grpc.Models
{
    public class GetAssetPortfolioTradesResponse
    {
        [DataMember(Order = 1)] public bool Success { get; set; }
        [DataMember(Order = 2)] public string ErrorMessage { get; set; }
        [DataMember(Order = 3)] public long IdForNextQuery { get; set; }
        [DataMember(Order = 4)] public List<AssetPortfolioTrade> Trades { get; set; }
    }
}