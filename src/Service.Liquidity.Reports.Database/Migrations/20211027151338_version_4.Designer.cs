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
    [Migration("20211027151338_version_4")]
    partial class version_4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lp_reports")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
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

                    b.Property<int>("Side")
                        .HasColumnType("integer");

                    b.Property<string>("Source")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.Property<decimal>("TotalReleasePnl")
                        .HasColumnType("numeric");

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

            modelBuilder.Entity("Service.Liquidity.Portfolio.Domain.Models.FeeShareSettlement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

                    b.Property<decimal>("ReleasedPnl")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("SettlementDate")
                        .HasColumnType("timestamp without time zone");

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

                    b.ToTable("feesharesettlementhistory");
                });

            modelBuilder.Entity("Service.Liquidity.Portfolio.Domain.Models.ManualSettlement", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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
                        .HasColumnType("timestamp without time zone");

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

                    b.ToTable("manualsettlementhistory");
                });
#pragma warning restore 612, 618
        }
    }
}
