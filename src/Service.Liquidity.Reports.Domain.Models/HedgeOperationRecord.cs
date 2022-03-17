using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Liquidity.Reports.Domain.Models
{
    public class HedgeOperationRecord
    {
        public string Id { get; set; }
        public decimal TargetVolume { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<HedgeTradeRecord> HedgeTrades { get; set; }

        public decimal GetTradedVolume()
        {
            return HedgeTrades?.Sum(t => t.BaseVolume) ?? 0;
        }
    }
}

