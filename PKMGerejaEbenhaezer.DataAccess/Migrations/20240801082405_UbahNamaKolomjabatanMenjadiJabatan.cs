using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahNamaKolomjabatanMenjadiJabatan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "jabatan",
                table: "PendetaTable",
                newName: "Jabatan");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGEqy1PBGL18f4nrin5TqEydvI9V+hjYQpzc7mxdMFO4n/J95UIhmIh7DXNTiO8XQA==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPYoG1HwMX28L1/BmMwQ+SbmQu0psrb3FeTQvRosdjlVlH9qg8MvIGUM9EvqQLpUwA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Jabatan",
                table: "PendetaTable",
                newName: "jabatan");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJ8+93b30IlcBClHysa2R5t46Kz9BEcPbIklA8hrry0/gbadnCs8VW/UwbCaCQbuGw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEMk4HeZ+90BHTxm59wv1iSAkFFdlE0Z9BH/vFD9q9dd75a/SAdmHNYTdjLIF4wDsw==");
        }
    }
}
