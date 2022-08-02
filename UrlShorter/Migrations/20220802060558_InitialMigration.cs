using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShorter.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "GenerateKey",
                startValue: 10L);

            migrationBuilder.CreateTable(
                name: "ShortUrls",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharedUrl = table.Column<string>(type: "nvarchar(450)", nullable: true, defaultValueSql: "NEXT VALUE FOR GenerateKey")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShortUrls", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrls_SharedUrl",
                table: "ShortUrls",
                column: "SharedUrl",
                unique: true,
                filter: "[SharedUrl] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShortUrls");

            migrationBuilder.DropSequence(
                name: "GenerateKey");
        }
    }
}
