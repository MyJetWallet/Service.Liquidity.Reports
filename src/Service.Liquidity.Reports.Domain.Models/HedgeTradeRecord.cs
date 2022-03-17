using System;
using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Domain.Models
{
    [DataContract]
    public class HedgeTradeRecord
    {
        [DataMember(Order = 1)] public string Id { get; set; }
        [DataMember(Order = 2)] public string HedgeOperationId { get; set; }
        [DataMember(Order = 3)] public string BaseAsset { get; set; }
        [DataMember(Order = 4)] public decimal BaseVolume { get; set; }
        [DataMember(Order = 5)] public string QuoteAsset { get; set; }
        [DataMember(Order = 6)] public decimal QuoteVolume { get; set; }
        [DataMember(Order = 7)] public string ExchangeName { get; set; }
        [DataMember(Order = 8)] public decimal Price { get; set; }
        [DataMember(Order = 9)] public DateTime CreatedDate { get; set; }
        [DataMember(Order = 10)] public string ExternalId { get; set; }
        [DataMember(Order = 11)] public string FeeAsset { get; set; }
        [DataMember(Order = 12)] public string FeeVolume { get; set; }

        [DataMember(Order = 13)] public HedgeOperationRecord HedgeOperation { get; set; }
    }
}