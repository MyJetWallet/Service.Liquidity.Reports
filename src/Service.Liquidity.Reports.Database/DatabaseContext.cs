using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Postgres;
using Service.Liquidity.Portfolio.Domain.Models;
using Service.Liquidity.Reports.Database.Extensions;
using Service.Liquidity.Reports.Domain.Models;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database
{
    public class DatabaseContext : MyDbContext
    {
        private Activity _activity;
        
        public const string Schema = "lp_reports";
        
        private const string AssetPortfolioTradeTableName = "assetportfoliotrades";
        private const string ChangeBalanceHistoryTableName = "changebalancehistory";
        private const string ManualSettlementHistoryTableName = "manualsettlementhistory";
        private const string FeeShareTableName = "feesharesettlementhistory";

        private DbSet<PortfolioChangeBalance> ChangeBalanceHistories { get; set; }
        private DbSet<Settlement> ManualSettlementHistories { get; set; }
        
        private DbSet<FeeShare> FeeShareSettlementHistories { get; set; }

        private DbSet<AssetPortfolioTrade> AssetPortfolioTrades { get; set; }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);

            SetTradeEntity(modelBuilder);
            SetChangeBalanceHistoryEntity(modelBuilder);
            SetManualSettlementHistoryEntity(modelBuilder);
            SetFeeShareSettlementHistoryEntity(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SetManualSettlementHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Settlement>().ToTable(ManualSettlementHistoryTableName);
            modelBuilder.Entity<Settlement>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<Settlement>().HasKey(e => e.Id);
            
            modelBuilder.Entity<Settlement>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<Settlement>().Property(e => e.WalletFrom).HasMaxLength(64);
            modelBuilder.Entity<Settlement>().Property(e => e.WalletTo).HasMaxLength(64);
            modelBuilder.Entity<Settlement>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<Settlement>().Property(e => e.VolumeFrom);
            modelBuilder.Entity<Settlement>().Property(e => e.VolumeTo);
            modelBuilder.Entity<Settlement>().Property(e => e.SettlementDate);
            modelBuilder.Entity<Settlement>().Property(e => e.Comment).HasMaxLength(256);
            modelBuilder.Entity<Settlement>().Property(e => e.User).HasMaxLength(64);
            modelBuilder.Entity<Settlement>().Property(e => e.ReleasedPnl);
        }

        private void SetFeeShareSettlementHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeeShare>().ToTable(FeeShareTableName);
            modelBuilder.Entity<FeeShare>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<FeeShare>().HasKey(e => e.Id);
            
            modelBuilder.Entity<FeeShare>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<FeeShare>().Property(e => e.WalletFrom).HasMaxLength(64);
            modelBuilder.Entity<FeeShare>().Property(e => e.WalletTo).HasMaxLength(64);
            modelBuilder.Entity<FeeShare>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<FeeShare>().Property(e => e.VolumeFrom);
            modelBuilder.Entity<FeeShare>().Property(e => e.VolumeTo);
            modelBuilder.Entity<FeeShare>().Property(e => e.SettlementDate);
            modelBuilder.Entity<FeeShare>().Property(e => e.Comment).HasMaxLength(2048);
            modelBuilder.Entity<FeeShare>().Property(e => e.ReferrerClientId).HasMaxLength(128);
        }
        private void SetTradeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetPortfolioTrade>().ToTable(AssetPortfolioTradeTableName);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<AssetPortfolioTrade>().HasKey(e => e.Id);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.TradeId).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.AssociateBrokerId).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.WalletName).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.AssociateSymbol).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.BaseAsset).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.QuoteAsset).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.Side);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.Price);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.BaseVolume);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.QuoteVolume);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.BaseVolumeInUsd);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.QuoteVolumeInUsd);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.DateTime);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.ErrorMessage).HasMaxLength(256);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.Source).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.Comment).HasMaxLength(256);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.User).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.TotalReleasePnl);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.FeeAsset).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTrade>().Property(e => e.FeeVolume);

            modelBuilder.Entity<AssetPortfolioTrade>().HasIndex(e => e.TradeId).IsUnique();
            modelBuilder.Entity<AssetPortfolioTrade>().HasIndex(e => e.Source);
            modelBuilder.Entity<AssetPortfolioTrade>().HasIndex(e => e.BaseAsset);
            modelBuilder.Entity<AssetPortfolioTrade>().HasIndex(e => e.QuoteAsset);
        }
        
        private void SetChangeBalanceHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioChangeBalance>().ToTable(ChangeBalanceHistoryTableName);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<PortfolioChangeBalance>().HasKey(e => e.Id);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.WalletName).HasMaxLength(64);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.Balance);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.BalanceBeforeUpdate);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.UpdateDate);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.Comment).HasMaxLength(256);
            modelBuilder.Entity<PortfolioChangeBalance>().Property(e => e.User).HasMaxLength(64);
        }
        
        public async Task SaveManualSettlementHistoryAsync(IEnumerable<Settlement> settlements)
        {
            await ManualSettlementHistories.AddRangeAsync(settlements);
            await SaveChangesAsync();
        }
        
        public async Task SaveFeeShareSettlementHistoryAsync(IEnumerable<FeeShare> settlements)
        {
            await FeeShareSettlementHistories.AddRangeAsync(settlements);
            await SaveChangesAsync();
        }
        public async Task SaveChangeBalanceHistoryAsync(IEnumerable<PortfolioChangeBalance> histories)
        {
            await ChangeBalanceHistories.AddRangeAsync(histories);
            await SaveChangesAsync();
        }
        public async Task<List<PortfolioChangeBalance>> GetChangeBalanceHistory()
        {
            return ChangeBalanceHistories.ToList();
        }
        public async Task<List<PortfolioSettlement>> GetManualSettlementHistory()
        {
            return ManualSettlementHistories
                .Select(e => e.ToPortfolioSettlement())
                .ToList();
        }
        
        public async Task<List<PortfolioFeeShare>> GetFeeShareSettlementHistory()
        {
            return FeeShareSettlementHistories
                .Select(e => e.ToPortfolioFeeShare())
                .ToList();
        }
        
        public override void Dispose()
        {
            _activity?.Dispose();
            base.Dispose();
        }

        public async Task SaveTradesAsync(IReadOnlyList<AssetPortfolioTrade> trades)
        {
            await AssetPortfolioTrades
                .UpsertRange(trades)
                .On(e => e.TradeId)
                .RunAsync();
        }


        public async Task<List<AssetPortfolioTrade>> GetAssetPortfolioTrades(long lastId, int batchSize, string assetFilter)
        {
            if (lastId != 0)
            {
                if (string.IsNullOrWhiteSpace(assetFilter))
                {
                    return AssetPortfolioTrades
                        .Where(trade => trade.Id < lastId)
                        .OrderByDescending(trade => trade.Id)
                        .Take(batchSize)
                        .ToList();
                }
                return AssetPortfolioTrades
                    .Where(trade => trade.Id < lastId && (trade.BaseAsset.Contains(assetFilter) || trade.QuoteAsset.Contains(assetFilter)))
                    .OrderByDescending(trade => trade.Id)
                    .Take(batchSize)
                    .ToList();
            }
            if (string.IsNullOrWhiteSpace(assetFilter))
            {
                return AssetPortfolioTrades
                    .OrderByDescending(trade => trade.Id)
                    .Take(batchSize)
                    .ToList();
            }
            return AssetPortfolioTrades
                .Where(trade => trade.BaseAsset.Contains(assetFilter) || trade.QuoteAsset.Contains(assetFilter))
                .OrderByDescending(trade => trade.Id)
                .Take(batchSize)
                .ToList();
        }
    }
}
