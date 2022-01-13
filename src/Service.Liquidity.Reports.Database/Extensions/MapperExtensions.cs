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
                ReleasedPnl = portfolioSettlement.ReleasedPnl
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
                ReleasedPnl = portfolioSettlement.ReleasedPnl
            };
        }

        public static FeeShare ToFeeShare(this PortfolioFeeShare portfolioFeeShare)
        {
            return new FeeShare
            {
                //Id = 0,
                BrokerId = portfolioFeeShare.BrokerId,
                WalletFrom = portfolioFeeShare.WalletFrom,
                WalletTo = portfolioFeeShare.WalletTo,
                Asset = portfolioFeeShare.Asset,
                VolumeFrom = portfolioFeeShare.VolumeFrom,
                VolumeTo = portfolioFeeShare.VolumeTo,
                Comment = portfolioFeeShare.Comment,
                ReferrerClientId = portfolioFeeShare.ReferrerClientId,
                SettlementDate = portfolioFeeShare.SettlementDate,
                ReleasedPnl = portfolioFeeShare.ReleasedPnl,
                OperationId = portfolioFeeShare.OperationId
            };
        }

        public static PortfolioFeeShare ToPortfolioFeeShare(this FeeShare feeShare)
        {
            return new PortfolioFeeShare
            {
                BrokerId = feeShare.BrokerId,
                WalletFrom = feeShare.WalletFrom,
                WalletTo = feeShare.WalletTo,
                Asset = feeShare.Asset,
                VolumeFrom = feeShare.VolumeFrom,
                VolumeTo = feeShare.VolumeTo,
                Comment = feeShare.Comment,
                ReferrerClientId = feeShare.ReferrerClientId,
                SettlementDate = feeShare.SettlementDate,
                ReleasedPnl = feeShare.ReleasedPnl,
                OperationId = feeShare.OperationId,
            };
        }
    }
}
