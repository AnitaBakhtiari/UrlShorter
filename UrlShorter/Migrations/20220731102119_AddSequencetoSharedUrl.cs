using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlShorter.Migrations
{
    public partial class AddSequencetoSharedUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "GenerateKey",
                startValue: 10L);

            migrationBuilder.AlterColumn<string>(
                name: "SharedUrl",
                table: "ShortUrls",
                type: "TEXT",
                nullable: true,
                defaultValueSql: "NEXT VALUE FOR GenerateKey",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "GenerateKey");

            migrationBuilder.AlterColumn<string>(
                name: "SharedUrl",
                table: "ShortUrls",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true,
                oldDefaultValueSql: "NEXT VALUE FOR OrderNumbers");
        }
    }
}
