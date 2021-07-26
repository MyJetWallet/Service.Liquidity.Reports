﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Service.Liquidity.Reports.Database;

namespace Service.Liquidity.Reports.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lp_reports")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Service.Liquidity.Portfolio.Domain.Models.AssetPortfolioTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

                    b.Property<string>("Comment")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ErrorMessage")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

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

                    b.Property<string>("WalletName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.HasIndex("BaseAsset");

                    b.HasIndex("QuoteAsset");

                    b.HasIndex("Source");

                    b.HasIndex("TradeId")
                        .IsUnique();

                    b.ToTable("assetportfoliotrades");
                });

            modelBuilder.Entity("Service.Liquidity.Portfolio.Domain.Models.ChangeBalanceHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Asset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("BalanceBeforeUpdate")
                        .HasColumnType("numeric");

                    b.Property<string>("BrokerId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Comment")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("User")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("VolumeDifference")
                        .HasColumnType("numeric");

                    b.Property<string>("WalletName")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("changebalancehistory");
                });

            modelBuilder.Entity("Service.Liquidity.Reports.Database.PnlByAssetEntity", b =>
                {
                    b.Property<string>("TradeId")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<string>("Asset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("Pnl")
                        .HasColumnType("numeric");

                    b.HasKey("TradeId", "Asset");

                    b.HasIndex("TradeId");

                    b.ToTable("assetportfoliotradepnl");
                });

            modelBuilder.Entity("Service.Liquidity.Reports.Database.PortfolioTradeEntity", b =>
                {
                    b.Property<string>("TradeId")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("AssociateBrokerId")
                        .HasColumnType("text");

                    b.Property<string>("AssociateClientId")
                        .HasColumnType("text");

                    b.Property<string>("AssociateSymbol")
                        .HasColumnType("text");

                    b.Property<string>("AssociateWalletId")
                        .HasColumnType("text");

                    b.Property<double>("BaseVolume")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsInternal")
                        .HasColumnType("boolean");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<double>("QuoteVolume")
                        .HasColumnType("double precision");

                    b.Property<string>("ReferenceId")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Side")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("TradeId");

                    b.HasIndex("DateTime");

                    b.HasIndex("DateTime", "Symbol");

                    b.ToTable("portfolio_trades");
                });

            modelBuilder.Entity("Service.Liquidity.Reports.Database.PositionAssociationEntity", b =>
                {
                    b.Property<string>("PositionId")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("TradeId")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("IsInternalTrade")
                        .HasColumnType("boolean");

                    b.Property<string>("Source")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("PositionId", "TradeId");

                    b.HasIndex("PositionId");

                    b.HasIndex("TradeId");

                    b.ToTable("portfolio_position_assotiation");
                });

            modelBuilder.Entity("Service.Liquidity.Reports.Database.PositionPortfolioEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("BaseAsset")
                        .HasColumnType("text");

                    b.Property<decimal>("BaseVolume")
                        .HasMaxLength(64)
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("CloseTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("OpenTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("PLUsd")
                        .HasColumnType("numeric");

                    b.Property<decimal>("QuoteAssetToUsdPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("QuoteVolume")
                        .HasColumnType("numeric");

                    b.Property<string>("QuotesAsset")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("ResultPercentage")
                        .HasColumnType("numeric");

                    b.Property<string>("Side")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Symbol")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("TotalBaseVolume")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TotalQuoteVolume")
                        .HasColumnType("numeric");

                    b.Property<string>("WalletId")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("CloseTime");

                    b.HasIndex("OpenTime");

                    b.HasIndex("IsOpen", "CloseTime");

                    b.ToTable("portfolio_position");
                });

            modelBuilder.Entity("Service.Liquidity.Reports.Database.PositionAssociationEntity", b =>
                {
                    b.HasOne("Service.Liquidity.Reports.Database.PositionPortfolioEntity", "Position")
                        .WithMany("Associations")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Service.Liquidity.Reports.Database.PortfolioTradeEntity", "Trade")
                        .WithMany("Associations")
                        .HasForeignKey("TradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Position");

                    b.Navigation("Trade");
                });

            modelBuilder.Entity("Service.Liquidity.Reports.Database.PortfolioTradeEntity", b =>
                {
                    b.Navigation("Associations");
                });

            modelBuilder.Entity("Service.Liquidity.Reports.Database.PositionPortfolioEntity", b =>
                {
                    b.Navigation("Associations");
                });
#pragma warning restore 612, 618
        }
    }
}
