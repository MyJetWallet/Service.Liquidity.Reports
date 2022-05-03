using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class SetupPortfolioTraderType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE lp_reports.assetportfoliotrades SET \"Type\"=1 WHERE \"Comment\" LIKE 'Swap%';");
            migrationBuilder.Sql("UPDATE lp_reports.assetportfoliotrades SET \"Type\"=2 WHERE \"Source\" LIKE '%anual';");
            migrationBuilder.Sql("UPDATE lp_reports.assetportfoliotrades SET \"Type\"=3 WHERE \"Source\" LIKE 'Hedger';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
