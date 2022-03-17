using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class RemoveUnusedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleasedPnl",
                schema: "lp_reports",
                table: "feesharesettlementhistory");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReleasedPnl",
                schema: "lp_reports",
                table: "feesharesettlementhistory",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
