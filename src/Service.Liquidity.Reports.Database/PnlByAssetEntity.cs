using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database
{
    public class PnlByAssetEntity
    {
        public string Asset { get; set; }
        public string TradeId { get; set; }
        public decimal Pnl { get; set; }

        public static PnlByAssetEntity CreateByParent(PnlByAsset parent, string parentId)
        {
            return new PnlByAssetEntity
            {
                Asset = parent.Asset,
                Pnl = parent.Pnl,
                TradeId = parentId
            };
        }
    }
}