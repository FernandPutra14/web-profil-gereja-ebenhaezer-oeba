using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahSeedingWarnaKategori : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Warna",
                value: -855310);

            migrationBuilder.UpdateData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "Warna",
                value: -1180943);

            migrationBuilder.UpdateData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "Warna",
                value: -331031);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
