using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class PortfolioChangeBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VolumeDifference",
                schema: "lp_reports",
                table: "changebalancehistory",
                newName: "Balance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Balance",
                schema: "lp_reports",
                table: "changebalancehistory",
                newName: "VolumeDifference");
        }
    }
}
