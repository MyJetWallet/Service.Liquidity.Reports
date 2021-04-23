using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Domain.Orders;

namespace Service.Liquidity.Reports.Database
{
    public class DatabaseContext : DbContext
    {
        public const string Schema = "lp_reports";

        public const string PortfolioTradesTableName = "portfolio_trades";
        public const string PositionAssociationsTableName = "portfolio_position_assotiation";
        public const string PositionTableName = "portfolio_position";

        public DbSet<PortfolioTradeEntity> PortfolioTrades { get; set; }

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


            base.OnModelCreating(modelBuilder);
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
    }
}
