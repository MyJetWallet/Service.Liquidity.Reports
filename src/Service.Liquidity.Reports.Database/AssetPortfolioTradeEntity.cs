using System.Collections.Generic;
using System.Linq;
using MyJetWallet.Domain.Orders;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database
{
    public class AssetPortfolioTradeEntity : AssetPortfolioTrade
    {
        public new List<PnlByAssetEntity> ReleasePnl { get; set; }

        public static AssetPortfolioTradeEntity CreateFromParent(AssetPortfolioTrade parent)
        {
            var result = new AssetPortfolioTradeEntity
            {
                AssociateBrokerId = parent.AssociateBrokerId,
                AssociateSymbol = parent.AssociateSymbol,
                BaseAsset = parent.BaseAsset,
                BaseAssetPriceInUsd = parent.BaseAssetPriceInUsd,
                BaseVolume = parent.BaseVolume,
                BaseVolumeInUsd = parent.BaseAssetPriceInUsd,
                Comment = parent.Comment,
                DateTime = parent.DateTime,
                ErrorMessage = parent.ErrorMessage,
                Id = parent.Id,
                Price = parent.Price,
                QuoteAsset = parent.QuoteAsset,
                QuoteAssetPriceInUsd = parent.QuoteAssetPriceInUsd,
                QuoteVolume = parent.QuoteVolume,
                QuoteVolumeInUsd = parent.QuoteVolumeInUsd,
                Side = parent.Side,
                Source = parent.Source,
                TradeId = parent.TradeId,
                User = parent.User,
                WalletName = parent.WalletName,
                ReleasePnl = parent.ReleasePnl?.Select(PnlByAssetEntity.CreateByParent).ToList()
            };
            result.ReleasePnl?.ForEach(e => e.TradeEntity = result);
            return result;
        }
        
        public static AssetPortfolioTrade CreateAsParent(AssetPortfolioTradeEntity child)
        {
            var result = new AssetPortfolioTrade
            {
                AssociateBrokerId = child.AssociateBrokerId,
                AssociateSymbol = child.AssociateSymbol,
                BaseAsset = child.BaseAsset,
                BaseAssetPriceInUsd = child.BaseAssetPriceInUsd,
                BaseVolume = child.BaseVolume,
                BaseVolumeInUsd = child.BaseAssetPriceInUsd,
                Comment = child.Comment,
                DateTime = child.DateTime,
                ErrorMessage = child.ErrorMessage,
                Id = child.Id,
                Price = child.Price,
                QuoteAsset = child.QuoteAsset,
                QuoteAssetPriceInUsd = child.QuoteAssetPriceInUsd,
                QuoteVolume = child.QuoteVolume,
                QuoteVolumeInUsd = child.QuoteVolumeInUsd,
                Side = child.Side,
                Source = child.Source,
                TradeId = child.TradeId,
                User = child.User,
                WalletName = child.WalletName
            };
            return result;
        }
    }
}