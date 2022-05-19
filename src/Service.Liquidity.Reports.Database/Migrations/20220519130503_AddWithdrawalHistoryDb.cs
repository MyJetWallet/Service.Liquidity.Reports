using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Service.Liquidity.Reports.Database.Migrations
{
    public partial class AddWithdrawalHistoryDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exchangewithdrawalshistory",
                schema: "lp_reports",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Exchange = table.Column<int>(type: "integer", maxLength: 64, nullable: false),
                    TxId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    InternalId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    ExchangeAsset = table.Column<string>(type: "text", nullable: true),
                    Asset = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Notes = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Fee = table.Column<decimal>(type: "numeric", nullable: false),
                    FeeInUsd = table.Column<decimal>(type: "numeric", nullable: false),
                    Volume = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exchangewithdrawalshistory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "ExchangeTxId",
                schema: "lp_reports",
                table: "exchangewithdrawalshistory",
                columns: new[] { "Exchange", "TxId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exchangewithdrawalshistory",
                schema: "lp_reports");
        }
    }
}
