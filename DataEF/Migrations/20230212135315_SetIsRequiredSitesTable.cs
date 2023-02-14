using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEF.Migrations
{
    /// <inheritdoc />
    public partial class SetIsRequiredSitesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "created",
                value: new DateTime(2023, 2, 12, 7, 53, 14, 986, DateTimeKind.Local).AddTicks(6127));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "created",
                value: new DateTime(2023, 2, 12, 7, 53, 14, 986, DateTimeKind.Local).AddTicks(6161));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "created",
                value: new DateTime(2023, 2, 12, 7, 53, 14, 986, DateTimeKind.Local).AddTicks(6164));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "created",
                value: new DateTime(2023, 2, 12, 7, 53, 14, 986, DateTimeKind.Local).AddTicks(6166));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "created",
                value: new DateTime(2023, 2, 12, 7, 53, 14, 986, DateTimeKind.Local).AddTicks(6168));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "created",
                value: new DateTime(2023, 2, 12, 7, 53, 14, 986, DateTimeKind.Local).AddTicks(6170));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 7,
                column: "created",
                value: new DateTime(2023, 2, 12, 7, 53, 14, 986, DateTimeKind.Local).AddTicks(6208));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 8,
                column: "created",
                value: new DateTime(2023, 2, 12, 7, 53, 14, 986, DateTimeKind.Local).AddTicks(6210));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "created",
                value: new DateTime(2023, 2, 11, 18, 3, 4, 399, DateTimeKind.Local).AddTicks(7705));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "created",
                value: new DateTime(2023, 2, 11, 18, 3, 4, 399, DateTimeKind.Local).AddTicks(7743));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "created",
                value: new DateTime(2023, 2, 11, 18, 3, 4, 399, DateTimeKind.Local).AddTicks(7746));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "created",
                value: new DateTime(2023, 2, 11, 18, 3, 4, 399, DateTimeKind.Local).AddTicks(7748));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "created",
                value: new DateTime(2023, 2, 11, 18, 3, 4, 399, DateTimeKind.Local).AddTicks(7750));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "created",
                value: new DateTime(2023, 2, 11, 18, 3, 4, 399, DateTimeKind.Local).AddTicks(7753));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 7,
                column: "created",
                value: new DateTime(2023, 2, 11, 18, 3, 4, 399, DateTimeKind.Local).AddTicks(7754));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 8,
                column: "created",
                value: new DateTime(2023, 2, 11, 18, 3, 4, 399, DateTimeKind.Local).AddTicks(7756));
        }
    }
}
