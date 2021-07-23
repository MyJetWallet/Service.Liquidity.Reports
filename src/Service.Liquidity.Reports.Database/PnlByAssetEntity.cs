using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database
{
    public class PnlByAssetEntity : PnlByAsset
    {
        public AssetPortfolioTradeEntity TradeEntity { get; set; }

        public static PnlByAssetEntity CreateByParent(PnlByAsset parent)
        {
            return new PnlByAssetEntity
            {
                Asset = parent.Asset,
                Id = parent.Id,
                Pnl = parent.Pnl
            };
        }
    }
}