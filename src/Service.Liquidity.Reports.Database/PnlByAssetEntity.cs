using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database
{
    public class PnlByAssetEntity : PnlByAsset
    {
        public long Id { get; set; }
        public AssetPortfolioTradeEntity TradeEntity { get; set; }
        public long TradeId { get; set; }

        public static PnlByAssetEntity CreateByParent(PnlByAsset parent)
        {
            return new PnlByAssetEntity
            {
                Asset = parent.Asset,
                Pnl = parent.Pnl
            };
        }
    }
}