using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEF.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreGlobalConfigDateExpreToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8176));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8209));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8211));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8213));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8215));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8217));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 7,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8219));

            migrationBuilder.InsertData(
                table: "GlobalConfigurations",
                columns: new[] { "Id", "Description", "Name", "Value", "active", "created", "updated" },
                values: new object[] { 8, "Mercado libre exprire date", "ACCESS_TOKEN_EXPIRE_DATE", "", true, new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8221), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 8);

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

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7582));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7584));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7587));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 7,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 34, 23, 516, DateTimeKind.Local).AddTicks(7589));
        }
    }
}
