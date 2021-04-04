using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Grpc.Models
{
    [DataContract]
    public class GetPositionTradesRequest
    {
        [DataMember(Order = 1)] public string PositionId { get; set; }
    }
}