using System.Collections.Generic;
using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Reports.Database
{
    public class PortfolioTradeEntity : PortfolioTrade
    {
        public List<PositionAssociationEntity> Associations { get; set; }
    }
}