using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahKolomLastChangedDiAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastChanged",
                table: "AppUserTable",
                type: "timestamp without time zone",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastChanged",
                table: "AppUserTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAED8SjIC3zIZNRDEfFOyMQItGyy9z3Js+dHxUuRKhYBEid40lS8h12wXnLDwvG5aueA==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEH/+BB5KUfPY5zDqsvmwNhCA8Zm1BHrO2UwNSYs8fgQWcuyPImU01YWH8Fv3sxAX5g==");
        }
    }
}
