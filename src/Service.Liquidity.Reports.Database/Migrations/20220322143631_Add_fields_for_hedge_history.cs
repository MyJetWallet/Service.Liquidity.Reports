using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class Add_fields_for_hedge_history : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Market",
                schema: "lp_reports",
                table: "HedgeTrades",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Side",
                schema: "lp_reports",
                table: "HedgeTrades",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TargetAsset",
                schema: "lp_reports",
                table: "HedgeOperations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TradedVolume",
                schema: "lp_reports",
                table: "HedgeOperations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Market",
                schema: "lp_reports",
                table: "HedgeTrades");

            migrationBuilder.DropColumn(
                name: "Side",
                schema: "lp_reports",
                table: "HedgeTrades");

            migrationBuilder.DropColumn(
                name: "TargetAsset",
                schema: "lp_reports",
                table: "HedgeOperations");

            migrationBuilder.DropColumn(
                name: "TradedVolume",
                schema: "lp_reports",
                table: "HedgeOperations");
        }
    }
}
