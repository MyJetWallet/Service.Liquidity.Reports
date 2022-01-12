using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Service.Liquidity.Reports.Database.Extensions;
using Service.Liquidity.Reports.Domain.Models;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Tests
{
    public class SettlementTest
    {
        [Test]
        public void MapPortfolioSettlemets()
        {
            var portfolioSettlements = new List<PortfolioSettlement>()
            {
                new PortfolioSettlement()
                {
                    Id = 1,
                    BrokerId = "JetWallet",
                    WalletFrom = "Converter",
                    WalletTo = "Binance",
                    Asset = "BTC",
                    VolumeFrom = -1.0m,
                    VolumeTo = 2.0m,
                    Comment = "Correction Converter to Binance",
                    User = "user@user",
                    SettlementDate = DateTime.UtcNow,
                    ReleasedPnl = 1.99m
                },
                new PortfolioSettlement()
                {
                    Id = 2,
                    BrokerId = "JetWallet",
                    WalletFrom = "Ftx",
                    WalletTo = "Binance",
                    Asset = "ETH",
                    VolumeFrom = -2.0m,
                    VolumeTo = 3.0m,
                    Comment = "Correction Ftx to Binance",
                    User = "user@user",
                    SettlementDate = DateTime.UtcNow,
                    ReleasedPnl = 1m
                },
                new PortfolioSettlement()
                {
                    Id = 3,
                    BrokerId = "JetWallet",
                    WalletFrom = "Binance",
                    WalletTo = "Converter",
                    Asset = "USD",
                    VolumeFrom = -999.0m,
                    VolumeTo = 999.0m,
                    Comment = "Correction Binance to Converter",
                    User = "user@user",
                    SettlementDate = DateTime.UtcNow,
                    ReleasedPnl = 2m
                }
            }.AsReadOnly();

            var settlements = portfolioSettlements.Select(e => e.ToSettlement());

            Assert.AreEqual(3, settlements.Count());
        }

        [Test]
        public void MapSettlemets()
        {
            var portfolioSettlements = new List<Settlement>()
            {
                new Settlement()
                {
                    Id = 1,
                    BrokerId = "JetWallet",
                    WalletFrom = "Converter",
                    WalletTo = "Binance",
                    Asset = "BTC",
                    VolumeFrom = -1.0m,
                    VolumeTo = 2.0m,
                    Comment = "Correction Converter to Binance",
                    User = "user@user",
                    SettlementDate = DateTime.UtcNow,
                    ReleasedPnl = 1.3m
                },
                new Settlement()
                {
                    Id = 2,
                    BrokerId = "JetWallet",
                    WalletFrom = "Ftx",
                    WalletTo = "Binance",
                    Asset = "ETH",
                    VolumeFrom = -2.0m,
                    VolumeTo = 3.0m,
                    Comment = "Correction Ftx to Binance",
                    User = "user@user",
                    SettlementDate = DateTime.UtcNow,
                    ReleasedPnl = 2m

                },
                new Settlement()
                {
                    Id = 3,
                    BrokerId = "JetWallet",
                    WalletFrom = "Binance",
                    WalletTo = "Converter",
                    Asset = "USD",
                    VolumeFrom = -999.0m,
                    VolumeTo = 999.0m,
                    Comment = "Correction Binance to Converter",
                    User = "user@user",
                    SettlementDate = DateTime.UtcNow,
                    ReleasedPnl = 3m
                }
            }.AsReadOnly();

            var settlements = portfolioSettlements.Select(e => e.ToPortfolioSettlement());

            Assert.AreEqual(3, settlements.Count());
        }
    }
}
