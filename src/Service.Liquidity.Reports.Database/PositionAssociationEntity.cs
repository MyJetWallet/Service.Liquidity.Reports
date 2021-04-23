using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Reports.Database
{
    public class PositionAssociationEntity : PositionAssociation
    {
        public PortfolioTradeEntity Trade { get; set; }
        public PositionPortfolioEntity Position { get; set; }
    }
}