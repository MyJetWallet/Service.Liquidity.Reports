using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Liquidity.Reports.Domain.Models
{
    public class FeeShare
    {
        public long Id { get; set; }
        public string BrokerId { get; set; }
        public string WalletFrom { get; set; }
        public string WalletTo { get; set; }
        public string Asset { get; set; }
        public decimal VolumeFrom { get; set; }
        public decimal VolumeTo { get; set; }
        public string Comment { get; set; }
        public string ReferrerClientId { get; set; }
        public DateTime SettlementDate { get; set; }
        public decimal ReleasedPnl { get; set; }
        public string OperationId { get; set; }
    }
}
