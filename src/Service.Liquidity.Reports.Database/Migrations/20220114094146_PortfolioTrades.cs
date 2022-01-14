using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class PortfolioTrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                schema: "lp_reports",
                table: "assetportfoliotrades");

            migrationBuilder.DropColumn(
                name: "TotalReleasePnl",
                schema: "lp_reports",
                table: "assetportfoliotrades");

            migrationBuilder.RenameColumn(
                name: "WalletName",
                schema: "lp_reports",
                table: "assetportfoliotrades",
                newName: "QuoteWalletName");

            migrationBuilder.AddColumn<string>(
                name: "BaseWalletName",
                schema: "lp_reports",
                table: "assetportfoliotrades",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseWalletName",
                schema: "lp_reports",
                table: "assetportfoliotrades");

            migrationBuilder.RenameColumn(
                name: "QuoteWalletName",
                schema: "lp_reports",
                table: "assetportfoliotrades",
                newName: "WalletName");

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                schema: "lp_reports",
                table: "assetportfoliotrades",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalReleasePnl",
                schema: "lp_reports",
                table: "assetportfoliotrades",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
