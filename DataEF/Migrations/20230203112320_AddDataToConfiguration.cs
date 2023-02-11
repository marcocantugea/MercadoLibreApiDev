using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataEF.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GlobalConfigurations",
                columns: new[] { "Id", "Description", "Name", "Value", "active", "created", "updated" },
                values: new object[,]
                {
                    { 1, "Mercado libre client id", "CLIENT_ID", "", true, new DateTime(2023, 2, 3, 5, 23, 20, 796, DateTimeKind.Local).AddTicks(9172), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Mercado libre client secret", "CLIENT_SECRET", "", true, new DateTime(2023, 2, 3, 5, 23, 20, 796, DateTimeKind.Local).AddTicks(9226), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Mercado libre code generated", "ML_CODE", "", true, new DateTime(2023, 2, 3, 5, 23, 20, 796, DateTimeKind.Local).AddTicks(9229), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
