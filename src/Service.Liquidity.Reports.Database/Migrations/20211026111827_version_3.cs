using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class version_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "feesharesettlementhistory",
                schema: "lp_reports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrokerId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    WalletFrom = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    WalletTo = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Asset = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    VolumeFrom = table.Column<decimal>(type: "numeric", nullable: false),
                    VolumeTo = table.Column<decimal>(type: "numeric", nullable: false),
                    Comment = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    ReferrerClientId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    SettlementDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ReleasedPnl = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feesharesettlementhistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feesharesettlementhistory",
                schema: "lp_reports");
        }
    }
}
