using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Grpc.Models
{
    [DataContract]
    public class GetPnlByTradeRequest
    {
        [DataMember(Order = 1)] public string TradeId { get; set; }
    }
}