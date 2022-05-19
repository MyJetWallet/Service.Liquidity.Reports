using System;
using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Domain.Models.Models;

[DataContract]
public class Withdrawal
{
    [DataMember(Order = 1)] public long Id { get; set; }
    [DataMember(Order = 2)] public ExchangeType Exchange { get; set; }
    [DataMember(Order = 3)] public string TxId  { get; set; }
    [DataMember(Order = 4)] public string InternalId { get; set; }
    [DataMember(Order = 5)] public string ExchangeAsset { get; set; }
    [DataMember(Order = 6)] public string Asset { get; set; }
    [DataMember(Order = 7)] public DateTime Date  { get; set; }
    [DataMember(Order = 8)] public string Notes { get; set; }
    [DataMember(Order = 9)] public decimal Fee { get; set; }
    [DataMember(Order = 10)] public decimal FeeInUsd { get; set; }
    [DataMember(Order = 11)] public decimal Volume { get; set; }
}