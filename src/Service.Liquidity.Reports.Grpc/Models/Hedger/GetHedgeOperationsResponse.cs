using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.Liquidity.Reports.Domain.Models;

namespace Service.Liquidity.Reports.Grpc.Models.Hedger;

[DataContract]
public class GetHedgeOperationsResponse
{
    [DataMember(Order = 1)] public bool IsError { get; set; }
    [DataMember(Order = 2)] public string ErrorMessage { get; set; }
    [DataMember(Order = 3)] public IEnumerable<HedgeOperationRecord> Items { get; set; }
}