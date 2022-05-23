using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.Liquidity.Reports.Domain.Models.Models;

namespace Service.Liquidity.Reports.Grpc.Models.Exchange;

[DataContract]
public class GetWithdrawalsHistoryRequest
{
    [DataMember(Order = 1)] public DateTime From { get; set; }
    [DataMember(Order = 2)] public DateTime To { get; set; }
    [DataMember(Order = 3)] public int Page { get; set; }
    [DataMember(Order = 4)] public int PageSize { get; set; }
    [DataMember(Order = 5)] public List<ExchangeType> ExchangeFilter { get; set; }
    [DataMember(Order = 6)] public List<string> AssetFilter { get; set; }
}