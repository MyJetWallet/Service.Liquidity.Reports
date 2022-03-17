using System;
using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Grpc.Models.Hedger;

[DataContract]
public class GetHedgeOperationsRequest
{
    [DataMember(Order = 1)] public DateTime? LastDate { get; set; }
    [DataMember(Order = 2)] public int BatchSize { get; set; }
}