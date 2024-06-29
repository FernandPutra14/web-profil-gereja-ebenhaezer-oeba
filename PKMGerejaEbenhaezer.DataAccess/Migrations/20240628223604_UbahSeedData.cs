using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEOZFQhgBrAPG3NWeYli/x1J8QVxjTVja4CYDNobGMWXydn9myvzkl/pRs+kaMcFegg==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PembuatId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PembuatId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "PembuatId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "PembuatId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "PembuatId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                column: "PembuatId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { 4, 1 });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { 5, 1 });

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "FotoKetuaId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "FotoKetuaId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "FotoKetuaId",
                value: 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                column: "PembuatId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PembuatId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "PembuatId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "PembuatId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "PembuatId",
                value: null);

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                column: "PembuatId",
                value: null);

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FotoId", "PembuatId" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "FotoKetuaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "FotoKetuaId",
                value: null);

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "FotoKetuaId",
                value: null);
        }
    }
}
