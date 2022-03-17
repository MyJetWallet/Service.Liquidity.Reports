using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Liquidity.Reports.Domain.Models;

namespace Service.Liquidity.Reports.Database.Configurations
{
    public class HedgeTradeRecordConfiguration : IEntityTypeConfiguration<HedgeTradeRecord>
    {
        public void Configure(EntityTypeBuilder<HedgeTradeRecord> builder)
        {
            builder.ToTable("HedgeTrades");

            builder.Property(e => e.Id);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Price);
            builder.Property(e => e.BaseAsset);
            builder.Property(e => e.BaseVolume);
            builder.Property(e => e.CreatedDate);
            builder.Property(e => e.ExchangeName);
            builder.Property(e => e.ExternalId);
            builder.Property(e => e.FeeAsset);
            builder.Property(e => e.FeeVolume);
            builder.Property(e => e.HedgeOperationId);
            builder.Property(e => e.QuoteAsset);
            builder.Property(e => e.QuoteVolume);

            builder
                .HasOne(e => e.HedgeOperation)
                .WithMany(e => e.HedgeTrades)
                .HasForeignKey(e => e.HedgeOperationId);

            builder.HasIndex(e => e.CreatedDate);
        }
    }
}