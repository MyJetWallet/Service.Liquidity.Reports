using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class AddPortfolioTraderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FeeAssetPriceInUsd",
                schema: "lp_reports",
                table: "assetportfoliotrades",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeVolumeInUsd",
                schema: "lp_reports",
                table: "assetportfoliotrades",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "lp_reports",
                table: "assetportfoliotrades",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeAssetPriceInUsd",
                schema: "lp_reports",
                table: "assetportfoliotrades");

            migrationBuilder.DropColumn(
                name: "FeeVolumeInUsd",
                schema: "lp_reports",
                table: "assetportfoliotrades");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "lp_reports",
                table: "assetportfoliotrades");
        }
    }
}
