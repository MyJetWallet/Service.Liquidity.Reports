using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class add_hedger_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HedgeOperations",
                schema: "lp_reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TargetVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HedgeOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HedgeTrades",
                schema: "lp_reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    HedgeOperationId = table.Column<string>(type: "text", nullable: true),
                    BaseAsset = table.Column<string>(type: "text", nullable: true),
                    BaseVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    QuoteAsset = table.Column<string>(type: "text", nullable: true),
                    QuoteVolume = table.Column<decimal>(type: "numeric", nullable: false),
                    ExchangeName = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExternalId = table.Column<string>(type: "text", nullable: true),
                    FeeAsset = table.Column<string>(type: "text", nullable: true),
                    FeeVolume = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HedgeTrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HedgeTrades_HedgeOperations_HedgeOperationId",
                        column: x => x.HedgeOperationId,
                        principalSchema: "lp_reports",
                        principalTable: "HedgeOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HedgeTrades_HedgeOperationId",
                schema: "lp_reports",
                table: "HedgeTrades",
                column: "HedgeOperationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HedgeTrades",
                schema: "lp_reports");

            migrationBuilder.DropTable(
                name: "HedgeOperations",
                schema: "lp_reports");
        }
    }
}
