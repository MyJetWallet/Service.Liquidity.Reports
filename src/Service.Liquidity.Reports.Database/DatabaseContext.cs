using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database
{
    public class DatabaseContext : DbContext
    {
        private Activity _activity;
        
        public const string Schema = "lp_reports";
        
        private const string AssetPortfolioTradeTableName = "assetportfoliotrades";
        private const string ChangeBalanceHistoryTableName = "changebalancehistory";
        private const string ManualSettlementHistoryTableName = "manualsettlementhistory";
        private const string FeeShareSettlementHistoryTableName = "feesharesettlementhistory";

        private DbSet<ChangeBalanceHistory> ChangeBalanceHistories { get; set; }
        private DbSet<ManualSettlement> ManualSettlementHistories { get; set; }
        
        private DbSet<FeeShareSettlement> FeeShareSettlementHistories { get; set; }

        private DbSet<AssetPortfolioTrade> AssetPortfolioTrades { get; set; }
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public static ILoggerFactory LoggerFactory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (LoggerFactory != null)
            {
                optionsBuilder.UseLoggerFactory(LoggerFactory).EnableSensitiveDataLogging();
            }
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
            modelBuilder.Entity<ManualSettlement>().ToTable(ManualSettlementHistoryTableName);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<ManualSettlement>().HasKey(e => e.Id);
            
            modelBuilder.Entity<ManualSettlement>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.WalletFrom).HasMaxLength(64);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.WalletTo).HasMaxLength(64);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.VolumeFrom);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.VolumeTo);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.SettlementDate);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.Comment).HasMaxLength(256);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.User).HasMaxLength(64);
            modelBuilder.Entity<ManualSettlement>().Property(e => e.ReleasedPnl);
        }

        private void SetFeeShareSettlementHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FeeShareSettlement>().ToTable(FeeShareSettlementHistoryTableName);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<FeeShareSettlement>().HasKey(e => e.Id);
            
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.WalletFrom).HasMaxLength(64);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.WalletTo).HasMaxLength(64);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.VolumeFrom);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.VolumeTo);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.SettlementDate);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.Comment).HasMaxLength(2048);
            modelBuilder.Entity<FeeShareSettlement>().Property(e => e.ReferrerClientId).HasMaxLength(128);
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
            modelBuilder.Entity<ChangeBalanceHistory>().ToTable(ChangeBalanceHistoryTableName);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<ChangeBalanceHistory>().HasKey(e => e.Id);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.WalletName).HasMaxLength(64);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.VolumeDifference);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.BalanceBeforeUpdate);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.UpdateDate);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.Comment).HasMaxLength(256);
            modelBuilder.Entity<ChangeBalanceHistory>().Property(e => e.User).HasMaxLength(64);
        }
        
        public async Task SaveManualSettlementHistoryAsync(IEnumerable<ManualSettlement> settlements)
        {
            await ManualSettlementHistories.AddRangeAsync(settlements);
            await SaveChangesAsync();
        }
        
        public async Task SaveFeeShareSettlementHistoryAsync(IEnumerable<FeeShareSettlement> settlements)
        {
            await FeeShareSettlementHistories.AddRangeAsync(settlements);
            await SaveChangesAsync();
        }
        public async Task SaveChangeBalanceHistoryAsync(IEnumerable<ChangeBalanceHistory> histories)
        {
            await ChangeBalanceHistories.AddRangeAsync(histories);
            await SaveChangesAsync();
        }
        public async Task<List<ChangeBalanceHistory>> GetChangeBalanceHistory()
        {
            return ChangeBalanceHistories.ToList();
        }
        public async Task<List<ManualSettlement>> GetManualSettlementHistory()
        {
            return ManualSettlementHistories.ToList();
        }
        
        public async Task<List<FeeShareSettlement>> GetFeeShareSettlementHistory()
        {
            return FeeShareSettlementHistories.ToList();
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
