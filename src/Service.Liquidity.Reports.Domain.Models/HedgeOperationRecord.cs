using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Service.Liquidity.Reports.Domain.Models
{
    [DataContract]
    public class HedgeOperationRecord
    {
        [DataMember(Order = 1)] public string Id { get; set; }
        [DataMember(Order = 2)] public decimal TargetVolume { get; set; }
        [DataMember(Order = 3)] public DateTime CreatedDate { get; set; }
        [DataMember(Order = 4)] public List<HedgeTradeRecord> HedgeTrades { get; set; }

        public decimal GetTradedVolume()
        {
            return HedgeTrades?.Sum(t => t.BaseVolume) ?? 0;
        }
    }
}