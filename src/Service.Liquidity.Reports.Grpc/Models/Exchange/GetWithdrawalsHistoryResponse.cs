using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.Liquidity.Hedger.Domain.Models;
using Service.Liquidity.Reports.Domain.Models.Models;

namespace Service.Liquidity.Reports.Grpc.Models.Exchange;

[DataContract]
public class GetWithdrawalsHistoryResponse
{
    [DataMember(Order = 1)] public bool IsError { get; set; }
    [DataMember(Order = 2)] public string ErrorMessage { get; set; }
    [DataMember(Order = 3)] public IEnumerable<Withdrawal> Items { get; set; }
}