using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTanggalDataSeedWarta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENCiedELQsD/4CmMEPzvf6FaWDnG/jloj7ilwaP6CzLL9Yjj3TAsdhVWB+rTfDfEtw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELY9i1MfpWY2rY/oeY1vxZgSpQgJKeORXY5m9wIe6XMQsxyro91K7Z2azuQzSA+FTA==");

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalWarta",
                value: new DateOnly(2024, 7, 14));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELgdlZhEW2bxDdGu84/xXn5bWqzaBFPBoZrvdo/YdWcwJ0xLFV7ZSbequgi+imDTAw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELzbGsorfZLX3oQAwA91hi6LNilvp1+GzBkyzwyYXW6h4HA3vOXbRaQE7bI4dtqVEQ==");

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalWarta",
                value: new DateOnly(2024, 7, 7));
        }
    }
}
