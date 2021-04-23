﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Service.Liquidity.Reports.Database;

namespace Service.Liquidity.Reports.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210423144504_Version_1")]
    partial class Version_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lp_reports")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Service.Liquidity.Reports.Database.PortfolioTradeEntity", b =>
                {
                    b.Property<string>("TradeId")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

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