using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PerbaikiSeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAhPXpdwb31iCHWi50OGMQkK1HQ7Z/7Qe+KTOGyGr6p7FG0u1ghY9j+zZs93jTtOkQ==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PathFotoKompresi",
                value: "wwwroot/img/pengumuman/natall.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPvZgNsZQnLlVMrAUvKEeG6K7iSSfGuDlDy76jLHdkPGq6najWTarp8AWQDby86QMw==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PathFotoKompresi",
                value: "/wwwroot/img/pengumuman/natall.jpg");
        }
    }
}
