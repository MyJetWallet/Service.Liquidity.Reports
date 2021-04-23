using System.Collections.Generic;
using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Reports.Database
{
    public class PortfolioTradeEntity : PortfolioTrade
    {
        public List<PositionAssociationEntity> Associations { get; set; }

        public PortfolioTrade AsPortfolioTrade()
        {
            return new PortfolioTrade()
            {
                DateTime = DateTime,
                BaseVolume = BaseVolume,
                IsInternal = IsInternal,
                Price = Price,
                QuoteVolume = QuoteVolume,
                ReferenceId = ReferenceId,
                Side = Side,
                Source = Source,
                Symbol = Symbol,
                TradeId = TradeId
            };
        }
    }
}