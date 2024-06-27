using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicLoreApi.Migrations
{
    /// <inheritdoc />
    public partial class EnumToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PowerTier",
                table: "Powers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PowerTier",
                value: "A");

            migrationBuilder.UpdateData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PowerTier",
                value: "S");

            migrationBuilder.UpdateData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PowerTier",
                value: "B");

            migrationBuilder.UpdateData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PowerTier",
                value: "C");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PowerTier",
                table: "Powers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PowerTier",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 2,
                column: "PowerTier",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 3,
                column: "PowerTier",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 4,
                column: "PowerTier",
                value: 3);
        }
    }
}
