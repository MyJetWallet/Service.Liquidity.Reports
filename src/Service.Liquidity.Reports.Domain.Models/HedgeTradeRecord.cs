using System;

namespace Service.Liquidity.Reports.Domain.Models
{
    public class HedgeTradeRecord
    {
        public string Id { get; set; }
        public string HedgeOperationId { get; set; }
        public string BaseAsset { get; set; }
        public decimal BaseVolume { get; set; }
        public string QuoteAsset { get; set; }
        public decimal QuoteVolume { get; set; }
        public string ExchangeName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ExternalId { get; set; }
        public string FeeAsset { get; set; }
        public string FeeVolume { get; set; }

        public HedgeOperationRecord HedgeOperation { get; set; }
    }
}