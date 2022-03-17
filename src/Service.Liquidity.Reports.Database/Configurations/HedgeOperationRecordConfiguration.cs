using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Liquidity.Reports.Domain.Models;

namespace Service.Liquidity.Reports.Database.Configurations
{
    public class HedgeOperationRecordConfiguration : IEntityTypeConfiguration<HedgeOperationRecord>
    {
        public void Configure(EntityTypeBuilder<HedgeOperationRecord> builder)
        {
            builder.ToTable("HedgeOperations");
            builder.Property(e => e.Id);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TargetVolume);

            builder
                .HasMany(e => e.HedgeTrades)
                .WithOne()
                .HasForeignKey(e => e.HedgeOperationId);

            builder.HasIndex(e => e.CreatedDate);
        }
    }
}