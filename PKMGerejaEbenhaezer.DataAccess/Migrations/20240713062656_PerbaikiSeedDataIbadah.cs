using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PerbaikiSeedDataIbadah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIhoMqNTzf43/qX1OaAA3mwrqMCLN4/w9awv7MmJzCMVxh+4RTsQYY6MAYcuiI+SCw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBUbTOjkFaIciuZyc6VE2zdyaOLmQO8t3Po14eOYORPqh27TSv9sZfvYOdRiOgeJuQ==");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Renungan",
                value: "Markus 3:4,15");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEWfv5RFvpPV4QbwMTYxvOL5aeD75tRcq0MpU1N5J/VRBdeX7Lra76RKtV2jwxIhqA==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJTA2H2BOok2TXqcB2eH3l/4Ux0etZIlwz/eS4x5wEv+qT14znQjR3LS+Y5Pf57Q7A==");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Renungan",
                value: "Markus 3:4-15");
        }
    }
}
