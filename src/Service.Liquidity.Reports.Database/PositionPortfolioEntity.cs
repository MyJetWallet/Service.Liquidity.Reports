using System.Collections.Generic;
using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Reports.Database
{
    public class PositionPortfolioEntity: PositionPortfolio
    {
        public List<PositionAssociationEntity> Associations { get; set; }

        public PositionPortfolio AsPositionPortfolio()
        {
            return new PositionPortfolio()
            {
                QuoteVolume = QuoteVolume,
                BaseVolume = BaseVolume,
                Side = Side,
                Symbol = Symbol,
                BaseAsset = BaseAsset,
                CloseTime = CloseTime,
                Id = Id,
                IsOpen = IsOpen,
                OpenTime = OpenTime,
                PLUsd = PLUsd,
                QuoteAssetToUsdPrice = QuoteAssetToUsdPrice,
                QuotesAsset = QuotesAsset,
                ResultPercentage = ResultPercentage,
                TotalBaseVolume = TotalBaseVolume,
                TotalQuoteVolume = TotalQuoteVolume,
                WalletId = WalletId
            };
        }
    }
}