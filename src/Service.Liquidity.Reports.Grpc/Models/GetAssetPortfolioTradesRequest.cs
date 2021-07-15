using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Grpc.Models
{
    [DataContract]
    public class GetAssetPortfolioTradesRequest
    {
        [DataMember(Order = 1)] public long LastId { get; set; }
        [DataMember(Order = 2)] public int BatchSize { get; set; }
        [DataMember(Order = 3)] public string AssetFilter { get; set; }
    }
}