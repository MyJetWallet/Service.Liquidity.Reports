using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Domain.Orders;
using Service.Liquidity.Portfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database
{
    public class DatabaseContext : DbContext
    {
        private Activity _activity;
        
        public const string Schema = "lp_reports";

        private const string PortfolioTradesTableName = "portfolio_trades";
        private const string PositionAssociationsTableName = "portfolio_position_assotiation";
        private const string PositionTableName = "portfolio_position";
        private const string AssetPortfolioTradeTableName = "assetportfoliotrades";
        private const string ChangeBalanceHistoryTableName = "changebalancehistory";
        private const string PnlByAssetTableName = "assetportfoliotradepnl";

        public DbSet<ChangeBalanceHistory> ChangeBalanceHistories { get; set; }

        public DbSet<PortfolioTradeEntity> PortfolioTrades { get; set; }
        private DbSet<AssetPortfolioTradeEntity> AssetPortfolioTrades { get; set; }
        private DbSet<PnlByAssetEntity> PnlByAssets { get; set; }

        public DbSet<PositionAssociationEntity> PositionAssociations { get; set; }

        public DbSet<PositionPortfolioEntity> Positions { get; set; }

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

            modelBuilder.Entity<PortfolioTradeEntity>().ToTable(PortfolioTradesTableName);
            modelBuilder.Entity<PortfolioTradeEntity>().HasKey(e => e.TradeId);
            modelBuilder.Entity<PortfolioTradeEntity>().Property(e => e.TradeId).HasMaxLength(128);
            modelBuilder.Entity<PortfolioTradeEntity>().Property(e => e.Source).HasMaxLength(256);
            modelBuilder.Entity<PortfolioTradeEntity>().Property(e => e.Symbol).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTradeEntity>().Property(e => e.ReferenceId).HasMaxLength(256);
            modelBuilder.Entity<PortfolioTradeEntity>().Property(e => e.Side).HasConversion(
                v => v.ToString(),
                v => (OrderSide)Enum.Parse(typeof(OrderSide), v));
            modelBuilder.Entity<PortfolioTradeEntity>().HasIndex(e => e.DateTime);
            modelBuilder.Entity<PortfolioTradeEntity>().HasIndex(e => new { e.DateTime, e.Symbol });


            modelBuilder.Entity<PositionAssociationEntity>().ToTable(PositionAssociationsTableName);
            modelBuilder.Entity<PositionAssociationEntity>().HasKey(e => new {e.PositionId, e.TradeId});
            modelBuilder.Entity<PositionAssociationEntity>().Property(e => e.TradeId).HasMaxLength(128);
            modelBuilder.Entity<PositionAssociationEntity>().Property(e => e.Source).HasMaxLength(256);
            modelBuilder.Entity<PositionAssociationEntity>().Property(e => e.PositionId).HasMaxLength(256);
            modelBuilder.Entity<PositionAssociationEntity>().HasIndex(e => e.PositionId);
            modelBuilder.Entity<PositionAssociationEntity>().HasIndex(e => e.TradeId);


            modelBuilder.Entity<PositionPortfolioEntity>().ToTable(PositionTableName);
            modelBuilder.Entity<PositionPortfolioEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<PositionPortfolioEntity>().Property(e => e.Id).HasMaxLength(128);
            modelBuilder.Entity<PositionPortfolioEntity>().Property(e => e.WalletId).HasMaxLength(128);
            modelBuilder.Entity<PositionPortfolioEntity>().Property(e => e.Symbol).HasMaxLength(64);
            modelBuilder.Entity<PositionPortfolioEntity>().Property(e => e.BaseVolume).HasMaxLength(64);
            modelBuilder.Entity<PositionPortfolioEntity>().Property(e => e.QuotesAsset).HasMaxLength(64);
            modelBuilder.Entity<PositionPortfolioEntity>().Property(e => e.Side).HasConversion(
                v => v.ToString(),
                v => (OrderSide)Enum.Parse(typeof(OrderSide), v));
            modelBuilder.Entity<PositionPortfolioEntity>().HasIndex(e => e.OpenTime);
            modelBuilder.Entity<PositionPortfolioEntity>().HasIndex(e => e.CloseTime);
            modelBuilder.Entity<PositionPortfolioEntity>().HasIndex(e => new {e.IsOpen, e.CloseTime});


            modelBuilder.Entity<PositionAssociationEntity>()
                .HasOne(e => e.Trade)
                .WithMany(e => e.Associations)
                .HasForeignKey(b => b.TradeId);

            modelBuilder.Entity<PositionAssociationEntity>()
                .HasOne(e => e.Position)
                .WithMany(e => e.Associations)
                .HasForeignKey(b => b.PositionId);


            SetTradeEntity(modelBuilder);
            SetChangeBalanceHistoryEntity(modelBuilder);
            SetPnlByAssetEntity(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }
        
        private void SetTradeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssetPortfolioTradeEntity>().ToTable(AssetPortfolioTradeTableName);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<AssetPortfolioTradeEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.TradeId).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.AssociateBrokerId).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.WalletName).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.AssociateSymbol).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.BaseAsset).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.QuoteAsset).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.Side);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.Price);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.BaseVolume);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.QuoteVolume);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.BaseVolumeInUsd);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.QuoteVolumeInUsd);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.DateTime);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.ErrorMessage).HasMaxLength(256);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.Source).HasMaxLength(64);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.Comment).HasMaxLength(256);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().Property(e => e.User).HasMaxLength(64);
            
            modelBuilder.Entity<AssetPortfolioTradeEntity>().HasIndex(e => e.TradeId).IsUnique();
            modelBuilder.Entity<AssetPortfolioTradeEntity>().HasIndex(e => e.Source);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().HasIndex(e => e.BaseAsset);
            modelBuilder.Entity<AssetPortfolioTradeEntity>().HasIndex(e => e.QuoteAsset);
        }
        
        private void SetPnlByAssetEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PnlByAssetEntity>().ToTable(PnlByAssetTableName);
            modelBuilder.Entity<PnlByAssetEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<PnlByAssetEntity>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<PnlByAssetEntity>().Property(e => e.Pnl);
            
            modelBuilder.Entity<PnlByAssetEntity>().HasIndex(e => e.Asset).IsUnique();
            
            modelBuilder.Entity<PnlByAssetEntity>()
                .HasOne(p => p.TradeEntity)
                .WithMany(b => b.ReleasePnl);
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

        public async Task<int> UpsetAsync(IEnumerable<PortfolioTradeEntity> entities)
        {
            var result = await PortfolioTrades.UpsertRange(entities).On(e => e.TradeId).NoUpdate().RunAsync();
            return result;
        }

        public async Task<int> UpsetAsync(IEnumerable<PositionAssociationEntity> entities)
        {
            var result = await PositionAssociations.UpsertRange(entities).On(e => new { e.PositionId, e.TradeId }).NoUpdate().RunAsync();
            return result;
        }

        public async Task<int> UpsetAsync(IEnumerable<PositionPortfolioEntity> entities)
        {
            var result = await Positions.UpsertRange(entities).On(e => e.Id).RunAsync();
            return result;
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
        
        public override void Dispose()
        {
            _activity?.Dispose();
            base.Dispose();
        }

        public async Task SaveTradesAsync(IReadOnlyList<AssetPortfolioTradeEntity> trades)
        {
            await AssetPortfolioTrades
                .UpsertRange(trades)
                .On(e => e.TradeId)
                .RunAsync();
            
            await PnlByAssets
                    .UpsertRange(trades.SelectMany(elem => elem.ReleasePnl ?? new List<PnlByAssetEntity>()))
                    .On(e => e.Id)
                    .RunAsync();
            
        }

        public async Task<List<AssetPortfolioTradeEntity>> GetAssetPortfolioTrades(long lastId, int batchSize, string assetFilter)
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
