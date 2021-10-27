using Microsoft.EntityFrameworkCore.Migrations;

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class version_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OperationId",
                schema: "lp_reports",
                table: "feesharesettlementhistory",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationId",
                schema: "lp_reports",
                table: "feesharesettlementhistory");
        }
    }
}
