using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PerbaikiSeedDataFoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDlSUGtS0u+xK2/Ay4aUUCDh/1Gn4DovXXmN2lWebWwWkeub5Z8/WLwZTNr2UFvXSw==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "/wwwroot/img/pengumuman/natall.jpg", "/wwwroot/img/pengumuman/natall.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "/wwwroot/img/pengumuman/rapatt.jpg", "/wwwroot/img/pengumuman/rapatt.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "/wwwroot/img/pengumuman/tripp.jpg", "/wwwroot/img/pengumuman/tripp.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "/wwwroot/img/pengumuman/donasii.jpg", "/wwwroot/img/pengumuman/donasii.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "/wwwroot/img/pengumuman/pelayanann.jpg", "/wwwroot/img/pengumuman/pelayanann.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEH9cuf1k9yw3tsqWUe0hzNNGrl2a4Z1FC0PjHzBYesiphrFxVNJDfxaX0LIq97uR6g==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\wwwroot\\img\\pengumuman\\natall.jpg", "\\wwwroot\\img\\pengumuman\\natall.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\wwwroot\\img\\pengumuman\\rapatt.jpg", "\\wwwroot\\img\\pengumuman\\rapatt.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\wwwroot\\img\\pengumuman\\tripp.jpg", "\\wwwroot\\img\\pengumuman\\tripp.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\wwwroot\\img\\pengumuman\\donasii.jpg", "\\wwwroot\\img\\pengumuman\\donasii.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\wwwroot\\img\\pengumuman\\pelayanann.jpg", "\\wwwroot\\img\\pengumuman\\pelayanann.jpg" });
        }
    }
}
