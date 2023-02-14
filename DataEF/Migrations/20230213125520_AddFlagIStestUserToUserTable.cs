using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEF.Migrations
{
    /// <inheritdoc />
    public partial class AddFlagIStestUserToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTestUser",
                table: "MLUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "created",
                value: new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1231));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 2,
                column: "created",
                value: new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1266));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 3,
                column: "created",
                value: new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1269));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 4,
                column: "created",
                value: new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1271));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 5,
                column: "created",
                value: new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1273));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 6,
                column: "created",
                value: new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1275));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 7,
                column: "created",
                value: new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1277));

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 8,
                column: "created",
                value: new DateTime(2023, 2, 13, 6, 55, 20, 155, DateTimeKind.Local).AddTicks(1279));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTestUser",
                table: "MLUsers");

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
    }
}
