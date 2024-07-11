using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahRenunganDiIbadah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Renungan",
                table: "IbadahTable",
                type: "text",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "Renungan",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Renungan",
                table: "IbadahTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENs5MoUz+CmkNbLye96mSUIcVABOtvHPhOOnaIx4n81JKTaNAbZfg261FKjl3812dw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEN1xDLj/NEtcPoKmqIJp8ZTbxYu20OkyADUmF80y6KZ7uoqbJxWMzvej/ZpytpDv9Q==");
        }
    }
}
