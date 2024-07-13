using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahTipeNasPembimbingDanRenunganMenjadiAyatAlkitab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "Renungan",
                value: "Markus 3:4-15");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOLwdcLM3y+b6dFwItNIYBWgij+P/GWuvT0YfUkyWJtoKK9dsCrFOTbxtmVEnwKXWw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPDDrcKfIfxtfG/PEt7lLa5nNFWHh1XWf5SOB7TQAZ2lC7lcb1PfC1dgCXuQbdQScg==");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Renungan",
                value: "Renungan 1");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "Renungan",
                value: "Renungan 1");
        }
    }
}
