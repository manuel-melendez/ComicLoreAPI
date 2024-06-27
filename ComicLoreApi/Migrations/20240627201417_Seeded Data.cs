using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComicLoreApi.Migrations
{
    /// <inheritdoc />
    public partial class SeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Powers",
                columns: new[] { "Id", "Description", "Name", "PowerTier" },
                values: new object[,]
                {
                    { 1, "Ability to fly", "Flight", 1 },
                    { 2, "Enhanced physical strength", "Super Strength", 0 },
                    { 3, "Ability to become invisible", "Invisibility", 2 },
                    { 4, "Use of advanced technology and gadgets", "Gadgets", 3 }
                });

            migrationBuilder.InsertData(
                table: "Supes",
                columns: new[] { "Id", "Alias", "DateOfBirth", "FirstName", "LastName", "Origin" },
                values: new object[,]
                {
                    { 1, "Superman", new DateTime(1938, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clark", "Kent", "Krypton" },
                    { 2, "Batman", new DateTime(1939, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bruce", "Wayne", "Gotham City" },
                    { 3, "Wonder Woman", new DateTime(1941, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diana", "Prince", "Themyscira" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Powers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Supes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Supes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Supes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
