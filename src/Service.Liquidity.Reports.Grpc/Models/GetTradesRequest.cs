using System;
using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Grpc.Models
{
    [DataContract]
    public class GetTradesRequest
    {
        [DataMember(Order = 1)] public DateTime LastSeenDateTime { get; set; }

        [DataMember(Order = 2)] public int? Take { get; set; }
    }
}