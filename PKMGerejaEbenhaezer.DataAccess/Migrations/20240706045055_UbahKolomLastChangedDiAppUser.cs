using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahKolomLastChangedDiAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastChanged",
                table: "AppUserTable",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastChanged", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAEBtqXD88FuipibsfI6BGsf0EiwvvzozmwvCvKDDJvmli0O4Flol3b7e6oGP/ofAT4Q==" });

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastChanged", "PasswordHash" },
                values: new object[] { new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAEGV88RS2VrTHO1uya97wkLooTKf2zULvgBX7Z9jfKnF0ICxTzDCFmkKejkOkKuKvUw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "LastChanged",
                table: "AppUserTable",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "LastChanged", "PasswordHash" },
                values: new object[] { null, "AQAAAAIAAYagAAAAENrfyOFb0JZeasp9pMWP3rs852pQK97KPCOxi/dmdP9Cl6P2opRxyekq5U1ZLZ4zxQ==" });

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "LastChanged", "PasswordHash" },
                values: new object[] { null, "AQAAAAIAAYagAAAAEN42l6ZUFofxLxmpqDqQAxz6az4X8Xr5qMueLwhL1XMVoB3qbuNnXY5M/ZGt6WwX4g==" });
        }
    }
}
