using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEF.Migrations
{
    /// <inheritdoc />
    public partial class AddSitesAndUserTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MLSites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefaultCurrencyId = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    MLId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MLSites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MLUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MLId = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    sitestatus = table.Column<string>(name: "site_status", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MLUsers", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MLSites");

            migrationBuilder.DropTable(
                name: "MLUsers");

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

            migrationBuilder.UpdateData(
                table: "GlobalConfigurations",
                keyColumn: "Id",
                keyValue: 8,
                column: "created",
                value: new DateTime(2023, 2, 11, 7, 50, 19, 67, DateTimeKind.Local).AddTicks(8221));
        }
    }
}
