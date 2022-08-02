using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShorter.Migrations
{
    public partial class intialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShortUrls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BaseUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SharedUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrls", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrls_SharedUrl",
                table: "ShortUrls",
                column: "SharedUrl",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortUrls");
        }
    }
}
