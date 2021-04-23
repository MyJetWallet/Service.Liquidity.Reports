﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class Version_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "lp_reports");

            migrationBuilder.CreateTable(
                name: "portfolio_position",
                schema: "lp_reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    WalletId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    IsOpen = table.Column<bool>(type: "boolean", nullable: false),
                    Symbol = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    BaseAsset = table.Column<string>(type: "text", nullable: true),
                    QuotesAsset = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Side = table.Column<string>(type: "text", nullable: false),
                    BaseVolume = table.Column<decimal>(type: "numeric", maxLength: 64, nullable: false),
                    QuoteVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    OpenTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CloseTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    QuoteAssetToUsdPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PLUsd = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalBaseVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalQuoteVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    ResultPercentage = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portfolio_position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "portfolio_trades",
                schema: "lp_reports",
                columns: table => new
                {
                    TradeId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Source = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsInternal = table.Column<bool>(type: "boolean", nullable: false),
                    Symbol = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Side = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    BaseVolume = table.Column<double>(type: "double precision", nullable: false),
                    QuoteVolume = table.Column<double>(type: "double precision", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReferenceId = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portfolio_trades", x => x.TradeId);
                });

            migrationBuilder.CreateTable(
                name: "portfolio_position_assotiation",
                schema: "lp_reports",
                columns: table => new
                {
                    PositionId = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    TradeId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Source = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsInternalTrade = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_portfolio_position_assotiation", x => new { x.PositionId, x.TradeId });
                    table.ForeignKey(
                        name: "FK_portfolio_position_assotiation_portfolio_position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "lp_reports",
                        principalTable: "portfolio_position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_portfolio_position_assotiation_portfolio_trades_TradeId",
                        column: x => x.TradeId,
                        principalSchema: "lp_reports",
                        principalTable: "portfolio_trades",
                        principalColumn: "TradeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_position_CloseTime",
                schema: "lp_reports",
                table: "portfolio_position",
                column: "CloseTime");

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_position_IsOpen_CloseTime",
                schema: "lp_reports",
                table: "portfolio_position",
                columns: new[] { "IsOpen", "CloseTime" });

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_position_OpenTime",
                schema: "lp_reports",
                table: "portfolio_position",
                column: "OpenTime");

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_position_assotiation_PositionId",
                schema: "lp_reports",
                table: "portfolio_position_assotiation",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_position_assotiation_TradeId",
                schema: "lp_reports",
                table: "portfolio_position_assotiation",
                column: "TradeId");

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_trades_DateTime",
                schema: "lp_reports",
                table: "portfolio_trades",
                column: "DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_portfolio_trades_DateTime_Symbol",
                schema: "lp_reports",
                table: "portfolio_trades",
                columns: new[] { "DateTime", "Symbol" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "portfolio_position_assotiation",
                schema: "lp_reports");

            migrationBuilder.DropTable(
                name: "portfolio_position",
                schema: "lp_reports");

            migrationBuilder.DropTable(
                name: "portfolio_trades",
                schema: "lp_reports");
        }
    }
}
