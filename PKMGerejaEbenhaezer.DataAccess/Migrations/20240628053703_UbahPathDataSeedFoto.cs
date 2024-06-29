using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahPathDataSeedFoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEJDmlQitfg7MUvbSCFEP0W485swas7UehvzYdPQhFzJRPTVT0kmNKyhDG4/Z+VDE8g==");

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

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "/wwwroot/img/generaluser.png", "/wwwroot/img/generaluser.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEAYCZ4x7qXvaV2XU9eQOPEM/0lAl1DcogrL4Z0C2mCCleBylRcCGUh1ajRJxUJGSiQ==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\img\\pengumuman\\natall.jpg", "\\img\\pengumuman\\natall.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\img\\pengumuman\\rapatt.jpg", "\\img\\pengumuman\\rapatt.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\img\\pengumuman\\tripp.jpg", "\\img\\pengumuman\\tripp.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\img\\pengumuman\\donasii.jpg", "\\img\\pengumuman\\donasii.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "\\img\\pengumuman\\pelayanann.jpg", "\\img\\pengumuman\\pelayanann.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PathFoto", "PathFotoKompresi" },
                values: new object[] { "/img/generaluser.png", "/img/generaluser.png" });
        }
    }
}
