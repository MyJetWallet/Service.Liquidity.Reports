using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class AddVolumeInUsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "VolumeInUsd",
                schema: "lp_reports",
                table: "exchangewithdrawalshistory",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeInUsd",
                schema: "lp_reports",
                table: "exchangewithdrawalshistory");
        }
    }
}
