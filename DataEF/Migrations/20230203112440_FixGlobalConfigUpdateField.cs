using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEF.Migrations
{
    /// <inheritdoc />
    public partial class FixGlobalConfigUpdateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated",
                table: "GlobalConfigurations",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2023, 2, 3, 5, 24, 40, 265, DateTimeKind.Local).AddTicks(2080), null });

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2023, 2, 3, 5, 24, 40, 265, DateTimeKind.Local).AddTicks(2113), null });

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2023, 2, 3, 5, 24, 40, 265, DateTimeKind.Local).AddTicks(2116), null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated",
                table: "GlobalConfigurations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2023, 2, 3, 5, 23, 20, 796, DateTimeKind.Local).AddTicks(9172), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2023, 2, 3, 5, 23, 20, 796, DateTimeKind.Local).AddTicks(9226), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "created", "updated" },
                values: new object[] { new DateTime(2023, 2, 3, 5, 23, 20, 796, DateTimeKind.Local).AddTicks(9229), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
