using System.Collections.Generic;
using Service.Liquidity.Engine.Domain.Models.Portfolio;

namespace Service.Liquidity.Reports.Database
{
    public class PositionPortfolioEntity: PositionPortfolio
    {
        public List<PositionAssociationEntity> Associations { get; set; }
    }
}