using System;

namespace Service.Liquidity.Reports.Domain.Models
{
    public class Settlement
    {
        public long Id { get; set; }
        public string BrokerId { get; set; }
        public string WalletFrom { get; set; }
        public string WalletTo { get; set; }
        public string Asset { get; set; }
        public Decimal VolumeFrom { get; set; }
        public Decimal VolumeTo { get; set; }
        public string Comment { get; set; }
        public string User { get; set; }
        public DateTime SettlementDate { get; set; }
        public Decimal ReleasedPnl { get; set; }
    }
}
