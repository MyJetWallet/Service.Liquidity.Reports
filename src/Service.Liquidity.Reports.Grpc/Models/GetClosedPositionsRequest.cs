using System;
using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Grpc.Models
{
    [DataContract]
    public class GetClosedPositionsRequest
    {
        [DataMember(Order = 1)] public DateTime LastSeenDateTime { get; set; }

        [DataMember(Order = 2)] public int? Take { get; set; }
    }
}