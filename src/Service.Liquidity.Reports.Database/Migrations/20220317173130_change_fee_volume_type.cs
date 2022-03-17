using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class change_fee_volume_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeVolume",
                schema: "lp_reports",
                table: "HedgeTrades");
            
            migrationBuilder.AddColumn<string>(
                name: "FeeVolume",
                schema: "lp_reports",
                table: "HedgeTrades",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FeeVolume",
                schema: "lp_reports",
                table: "HedgeTrades",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }
    }
}
