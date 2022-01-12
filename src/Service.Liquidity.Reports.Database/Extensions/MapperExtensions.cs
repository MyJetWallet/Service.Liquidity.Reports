using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Liquidity.Reports.Domain.Models;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database.Extensions
{
    public static class MapperExtensions
    {
        public static Settlement ToSettlement(this PortfolioSettlement portfolioSettlement)
        {
            return new Settlement
            {
                Id = portfolioSettlement.Id,
                BrokerId = portfolioSettlement.BrokerId,
                WalletFrom = portfolioSettlement.WalletFrom,
                WalletTo = portfolioSettlement.WalletTo,
                Asset = portfolioSettlement.Asset,
                VolumeFrom = portfolioSettlement.VolumeFrom,
                VolumeTo = portfolioSettlement.VolumeTo,
                Comment = portfolioSettlement.Comment,
                User = portfolioSettlement.User,
                SettlementDate = portfolioSettlement.SettlementDate,
                ReleasedPnl = 0 //portfolioSettlement.ReleasedPnl
            };
        }

        public static PortfolioSettlement ToPortfolioSettlement(this Settlement portfolioSettlement)
        {
            return new PortfolioSettlement
            {
                Id = portfolioSettlement.Id,
                BrokerId = portfolioSettlement.BrokerId,
                WalletFrom = portfolioSettlement.WalletFrom,
                WalletTo = portfolioSettlement.WalletTo,
                Asset = portfolioSettlement.Asset,
                VolumeFrom = portfolioSettlement.VolumeFrom,
                VolumeTo = portfolioSettlement.VolumeTo,
                Comment = portfolioSettlement.Comment,
                User = portfolioSettlement.User,
                SettlementDate = portfolioSettlement.SettlementDate,
                //ReleasedPnl = portfolioSettlement.ReleasedPnl
            };
        }
    }
}
