using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahWarnaKeKategoriIbadah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Warna",
                table: "KategoriIbadahTable",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENSjremJ0jBo5eIsySwzw6DqMMpk2HESVaMVf/dk8E1hbGCSrnFZlhYqkqSeta95wA==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEORpKyA7cnWcwRp2PsFULdgfwrajMBLMDaz++OzVtyW68iekgfvPJHIyHr2gzCf3Qw==");

            migrationBuilder.UpdateData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Warna",
                value: -16776961);

            migrationBuilder.UpdateData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "Warna",
                value: -16744448);

            migrationBuilder.UpdateData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "Warna",
                value: -65536);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Warna",
                table: "KategoriIbadahTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFmE+1Gc+hw57HUzZNfk2k0mF74F765O94z9cNWMjOky4KP/+TJuBVTX1r2hTICunw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEE+1BOcV5GJFHTNGUhASdjhy9O8qFUy5MCH1idKZIYEFIHMszsQNQ79RgFZEDiOjEg==");
        }
    }
}
