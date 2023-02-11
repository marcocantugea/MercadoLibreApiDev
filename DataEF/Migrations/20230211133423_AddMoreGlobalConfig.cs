using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataEF.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreGlobalConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7545));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7577));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7580));

            migrationBuilder.InsertData(
                table: "GlobalConfigurations",
                columns: new[] { "Id", "Description", "Name", "Value", "active", "created", "updated" },
                values: new object[,]
                {
                    { 4, "Mercado libre token generated", "ACCESS_TOKEN", "", true, new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7582), null },
                    { 5, "Mercado libre token exprire time miliseconds", "ACCESS_TOKEN_EXPIRE", "", true, new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7584), null },
                    { 6, "Mercado libre token user id", "ACCESS_TOKEN_USERID", "", true, new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7587), null },
                    { 7, "Mercado libre refresh token", "REFRESH_TOKEN", "", true, new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7589), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "created",
                value: new DateTime(2023, 2, 3, 5, 24, 40, 265, DateTimeKind.Local).AddTicks(2080));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "created",
                value: new DateTime(2023, 2, 3, 5, 24, 40, 265, DateTimeKind.Local).AddTicks(2113));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "created",
                value: new DateTime(2023, 2, 3, 5, 24, 40, 265, DateTimeKind.Local).AddTicks(2116));
        }
    }
}
