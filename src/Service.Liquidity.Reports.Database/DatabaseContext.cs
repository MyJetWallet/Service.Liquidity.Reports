using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;
using Service.Liquidity.Hedger.Domain.Models;
using Service.Liquidity.Reports.Domain.Models.Models;
using Service.Liquidity.TradingPortfolio.Domain.Models;

namespace Service.Liquidity.Reports.Database
{
    public class DatabaseContext : MyDbContext
    {
        public Activity _activity;

        public const string Schema = "lp_reports";
        
        private const string AssetPortfolioTradeTableName = "assetportfoliotrades";
        private const string ChangeBalanceHistoryTableName = "changebalancehistory";
        private const string ManualSettlementHistoryTableName = "manualsettlementhistory";
        private const string FeeShareTableName = "feesharesettlementhistory";
        private const string ExchangeWithdrawalsTableName = "exchangewithdrawalshistory";

        private DbSet<PortfolioChangeBalance> ChangeBalanceHistories { get; set; }
        private DbSet<PortfolioSettlement> ManualSettlementHistories { get; set; }
        private DbSet<PortfolioFeeShare> FeeShareSettlementHistories { get; set; }
        private DbSet<PortfolioTrade> AssetPortfolioTrades { get; set; }
        public DbSet<HedgeOperation> HedgeOperations { get; set; }
        public DbSet<HedgeTrade> HedgeTrades { get; set; }
        public DbSet<Withdrawal> ExchangeWithdrawals { get; set; }

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
            SetExchangeWithdrawalHistoryEntity(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }

        private void SetManualSettlementHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioSettlement>().ToTable(ManualSettlementHistoryTableName);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<PortfolioSettlement>().HasKey(e => e.Id);
            
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.WalletFrom).HasMaxLength(64);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.WalletTo).HasMaxLength(64);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.VolumeFrom);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.VolumeTo);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.SettlementDate);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.Comment).HasMaxLength(256);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.User).HasMaxLength(64);
            modelBuilder.Entity<PortfolioSettlement>().Property(e => e.ReleasedPnl);
        }

        private void SetFeeShareSettlementHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioFeeShare>().ToTable(FeeShareTableName);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<PortfolioFeeShare>().HasKey(e => e.Id);
            
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.WalletFrom).HasMaxLength(64);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.WalletTo).HasMaxLength(64);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.VolumeFrom);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.VolumeTo);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.SettlementDate);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.Comment).HasMaxLength(2048);
            modelBuilder.Entity<PortfolioFeeShare>().Property(e => e.ReferrerClientId).HasMaxLength(128);
        }
        private void SetTradeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioTrade>().ToTable(AssetPortfolioTradeTableName);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<PortfolioTrade>().HasKey(e => e.Id);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.TradeId).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.AssociateBrokerId).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.BaseWalletName).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.QuoteWalletName).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.AssociateSymbol).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.BaseAsset).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.QuoteAsset).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.Side);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.Price);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.BaseVolume);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.QuoteVolume);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.BaseVolumeInUsd);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.QuoteVolumeInUsd);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.DateTime);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.Source).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.Comment).HasMaxLength(256);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.User).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.FeeAsset).HasMaxLength(64);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.FeeVolume);
            
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.FeeAssetPriceInUsd);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.FeeVolumeInUsd);
            modelBuilder.Entity<PortfolioTrade>().Property(e => e.Type);
            
            modelBuilder.Entity<PortfolioTrade>().HasIndex(e => e.TradeId).IsUnique();
            modelBuilder.Entity<PortfolioTrade>().HasIndex(e => e.Source);
            modelBuilder.Entity<PortfolioTrade>().HasIndex(e => e.BaseAsset);
            modelBuilder.Entity<PortfolioTrade>().HasIndex(e => e.QuoteAsset);
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
        
        private void SetExchangeWithdrawalHistoryEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Withdrawal>().ToTable(ExchangeWithdrawalsTableName);
            
            modelBuilder.Entity<Withdrawal>().HasKey(e => e.Id );
            modelBuilder.Entity<Withdrawal>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<Withdrawal>().HasIndex(e => new {e.Exchange, e.TxId}, "ExchangeTxId").IsUnique();
            modelBuilder.Entity<Withdrawal>().Property(e => e.TxId).HasMaxLength(64);
            modelBuilder.Entity<Withdrawal>().Property(e => e.InternalId).HasMaxLength(64);
            modelBuilder.Entity<Withdrawal>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<Withdrawal>().Property(e => e.Date);
            modelBuilder.Entity<Withdrawal>().Property(e => e.Notes).HasMaxLength(64);
            modelBuilder.Entity<Withdrawal>().Property(e => e.Fee);
            modelBuilder.Entity<Withdrawal>().Property(e => e.FeeInUsd);
            modelBuilder.Entity<Withdrawal>().Property(e => e.Volume);
            modelBuilder.Entity<Withdrawal>().Property(e => e.Exchange).HasMaxLength(64);
            modelBuilder.Entity<Withdrawal>().Property(e => e.VolumeInUsd);

        }

        public async Task SaveManualSettlementHistoryAsync(IEnumerable<PortfolioSettlement> settlements)
        {
            await ManualSettlementHistories.AddRangeAsync(settlements);
            await SaveChangesAsync();
        }
        
        public async Task SaveFeeShareSettlementHistoryAsync(IEnumerable<PortfolioFeeShare> settlements)
        {
            await FeeShareSettlementHistories.AddRangeAsync(settlements);
            await SaveChangesAsync();
        }
        public async Task SaveChangeBalanceHistoryAsync(IEnumerable<PortfolioChangeBalance> histories)
        {
            await ChangeBalanceHistories.AddRangeAsync(histories);
            await SaveChangesAsync();
        }
        public Task<List<PortfolioChangeBalance>> GetChangeBalanceHistory()
        {
            return Task.FromResult(ChangeBalanceHistories.ToList());
        }
        public Task<List<PortfolioSettlement>> GetManualSettlementHistory()
        {
            return Task.FromResult(ManualSettlementHistories.ToList());
        }
        
        public Task<List<PortfolioFeeShare>> GetFeeShareSettlementHistory()
        {
            return Task.FromResult(FeeShareSettlementHistories.ToList());
        }
        
        public override void Dispose()
        {
            _activity?.Dispose();
            base.Dispose();
        }

        public async Task SaveTradesAsync(IReadOnlyList<PortfolioTrade> trades)
        {
            await AssetPortfolioTrades
                .UpsertRange(trades)
                .On(e => e.TradeId)
                .RunAsync();
        }
        
        public async Task<List<PortfolioTrade>> GetAssetPortfolioTrades(long lastId, int batchSize, string assetFilter,
            List<PortfolioTradeType> requestTypeFilter = null)
        {
            var query = AssetPortfolioTrades.AsNoTracking();
            if (lastId > 0)
            {
                query = query.Where(trade => trade.Id < lastId);
            }

            if (!string.IsNullOrWhiteSpace(assetFilter))
            {
                query = query.Where(trade => (trade.BaseAsset.Contains(assetFilter) || trade.QuoteAsset.Contains(assetFilter)));
            }

            if (requestTypeFilter !=null)
            {
                query = query.Where(trade => requestTypeFilter.Contains(trade.Type));
            }
            
            var result = await query
                .OrderByDescending(trade => trade.Id)
                .Take(batchSize)
                .ToListAsync();
            
            return result;
        }
        
        public async Task<(IEnumerable<Withdrawal>, int)> GetExchangeWithdrawalsHistoryAsync(
            DateTime from, DateTime to, int page, int pageSize, 
            List<ExchangeType> requestTypeFilter = null)
        {
            var query = ExchangeWithdrawals.AsNoTracking();
            if (requestTypeFilter !=null)
            {
                query = query.Where(item => requestTypeFilter.Contains(item.Exchange));
            }

            var resultTotalCount = await query
                .Where(w => w.Date >= from && w.Date < to)
                .CountAsync();
            
            var result = await query
                .Where(w => w.Date >= from && w.Date < to)
                .OrderByDescending(item => item.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            return (result, resultTotalCount);
        }
        
        public async Task SaveExchangeWithdrawalsHistoryAsync(IEnumerable<Withdrawal> withdrawals)
        {
            await ExchangeWithdrawals.AddRangeAsync(withdrawals);
            await SaveChangesAsync();
        }
        
        public async Task GetExchangeWithdrawalsHistoryNextAsync(IEnumerable<Withdrawal> withdrawals)
        {
            await ExchangeWithdrawals.AddRangeAsync(withdrawals);
            await SaveChangesAsync();
        }
    }
}
