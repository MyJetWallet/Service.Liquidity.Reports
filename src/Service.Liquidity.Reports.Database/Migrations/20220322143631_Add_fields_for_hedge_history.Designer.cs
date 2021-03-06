// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Service.Liquidity.Reports.Database;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220322143631_Add_fields_for_hedge_history")]
    partial class Add_fields_for_hedge_history
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lp_reports")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Service.Liquidity.Hedger.Domain.Models.HedgeOperation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TargetAsset")
                        .HasColumnType("text");

                    b.Property<decimal>("TargetVolume")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TradedVolume")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("HedgeOperations", "lp_reports");
                });

            modelBuilder.Entity("Service.Liquidity.Hedger.Domain.Models.HedgeTrade", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("BaseAsset")
                        .HasColumnType("text");

                    b.Property<decimal>("BaseVolume")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExchangeName")
                        .HasColumnType("text");

                    b.Property<string>("ExternalId")
                        .HasColumnType("text");

                    b.Property<string>("FeeAsset")
                        .HasColumnType("text");

                    b.Property<decimal>("FeeVolume")
                        .HasColumnType("numeric");

                    b.Property<string>("HedgeOperationId")
                        .HasColumnType("text");

                    b.Property<string>("Market")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("QuoteAsset")
                        .HasColumnType("text");

                    b.Property<decimal>("QuoteVolume")
                        .HasColumnType("numeric");

                    b.Property<int>("Side")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HedgeOperationId");

                    b.ToTable("HedgeTrades", "lp_reports");
                });

            modelBuilder.Entity("Service.Liquidity.TradingPortfolio.Domain.Models.PortfolioChangeBalance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Asset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BalanceBeforeUpdate")
                        .HasColumnType("numeric");

                    b.Property<string>("BrokerId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Comment")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("User")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("WalletName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("changebalancehistory", "lp_reports");
                });

            modelBuilder.Entity("Service.Liquidity.TradingPortfolio.Domain.Models.PortfolioFeeShare", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Asset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("BrokerId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Comment")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("OperationId")
                        .HasColumnType("text");

                    b.Property<string>("ReferrerClientId")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime>("SettlementDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("VolumeFrom")
                        .HasColumnType("numeric");

                    b.Property<decimal>("VolumeTo")
                        .HasColumnType("numeric");

                    b.Property<string>("WalletFrom")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("WalletTo")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("feesharesettlementhistory", "lp_reports");
                });

            modelBuilder.Entity("Service.Liquidity.TradingPortfolio.Domain.Models.PortfolioSettlement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Asset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("BrokerId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Comment")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<decimal>("ReleasedPnl")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("SettlementDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("User")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("VolumeFrom")
                        .HasColumnType("numeric");

                    b.Property<decimal>("VolumeTo")
                        .HasColumnType("numeric");

                    b.Property<string>("WalletFrom")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("WalletTo")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("manualsettlementhistory", "lp_reports");
                });

            modelBuilder.Entity("Service.Liquidity.TradingPortfolio.Domain.Models.PortfolioTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AssociateBrokerId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("AssociateSymbol")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("BaseAsset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("BaseAssetPriceInUsd")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BaseVolume")
                        .HasColumnType("numeric");

                    b.Property<decimal>("BaseVolumeInUsd")
                        .HasColumnType("numeric");

                    b.Property<string>("BaseWalletName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Comment")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FeeAsset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("FeeVolume")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("QuoteAsset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("QuoteAssetPriceInUsd")
                        .HasColumnType("numeric");

                    b.Property<decimal>("QuoteVolume")
                        .HasColumnType("numeric");

                    b.Property<decimal>("QuoteVolumeInUsd")
                        .HasColumnType("numeric");

                    b.Property<string>("QuoteWalletName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<int>("Side")
                        .HasColumnType("integer");

                    b.Property<string>("Source")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("TradeId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("User")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("BaseAsset");

                    b.HasIndex("QuoteAsset");

                    b.HasIndex("Source");

                    b.HasIndex("TradeId")
                        .IsUnique();

                    b.ToTable("assetportfoliotrades", "lp_reports");
                });

            modelBuilder.Entity("Service.Liquidity.Hedger.Domain.Models.HedgeTrade", b =>
                {
                    b.HasOne("Service.Liquidity.Hedger.Domain.Models.HedgeOperation", null)
                        .WithMany("HedgeTrades")
                        .HasForeignKey("HedgeOperationId");
                });

            modelBuilder.Entity("Service.Liquidity.Hedger.Domain.Models.HedgeOperation", b =>
                {
                    b.Navigation("HedgeTrades");
                });
#pragma warning restore 612, 618
        }
    }
}
