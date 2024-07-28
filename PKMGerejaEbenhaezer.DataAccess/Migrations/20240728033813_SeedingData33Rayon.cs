using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData33Rayon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHXUwQAcXhQlhC6PYbRkqZB9oLRL3e1fU3KngE4mvW+iGBSwj/swXY4SXRTablY5fw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELlA/njNKGlZ9B3Jlx9Mtwzbsd2Y9AwgdWGiMYUn1JMTLGcSIaaC1728HawkPYriKA==");

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "KetuaRayon", "Nama" },
                values: new object[] { "Ketua Rayon 1", "Rayon 1" });

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KetuaRayon", "Nama" },
                values: new object[] { "Ketua Rayon 2", "Rayon 2" });

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "KetuaRayon", "Nama" },
                values: new object[] { "Ketua Rayon 3", "Rayon 3" });

            migrationBuilder.InsertData(
                table: "RayonTable",
                columns: new[] { "Id", "FotoKetuaId", "JumlahAnak", "JumlahDewasa", "JumlahLakiLaki", "JumlahLansia", "JumlahPemuda", "JumlahPerempuan", "JumlahRemaja", "KetuaRayon", "Nama", "NoWa" },
                values: new object[,]
                {
                    { 4, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 4", "Rayon 4", "081234567891" },
                    { 5, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 5", "Rayon 5", "081234567891" },
                    { 6, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 6", "Rayon 6", "081234567891" },
                    { 7, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 7", "Rayon 7", "081234567891" },
                    { 8, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 8", "Rayon 8", "081234567891" },
                    { 9, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 9", "Rayon 9", "081234567891" },
                    { 10, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 10", "Rayon 10", "081234567891" },
                    { 11, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 11", "Rayon 11", "081234567891" },
                    { 12, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 12", "Rayon 12", "081234567891" },
                    { 13, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 13", "Rayon 13", "081234567891" },
                    { 14, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 14", "Rayon 14", "081234567891" },
                    { 15, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 15", "Rayon 15", "081234567891" },
                    { 16, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 16", "Rayon 16", "081234567891" },
                    { 17, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 17", "Rayon 17", "081234567891" },
                    { 18, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 18", "Rayon 18", "081234567891" },
                    { 19, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 19", "Rayon 19", "081234567891" },
                    { 20, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 20", "Rayon 20", "081234567891" },
                    { 21, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 21", "Rayon 21", "081234567891" },
                    { 22, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 22", "Rayon 22", "081234567891" },
                    { 23, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 23", "Rayon 23", "081234567891" },
                    { 24, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 24", "Rayon 24", "081234567891" },
                    { 25, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 25", "Rayon 25", "081234567891" },
                    { 26, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 26", "Rayon 26", "081234567891" },
                    { 27, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 27", "Rayon 27", "081234567891" },
                    { 28, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 28", "Rayon 28", "081234567891" },
                    { 29, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 29", "Rayon 29", "081234567891" },
                    { 30, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 30", "Rayon 30", "081234567891" },
                    { 31, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 31", "Rayon 31", "081234567891" },
                    { 32, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 32", "Rayon 32", "081234567891" },
                    { 33, 6, 10, 15, 25, 15, 10, 35, 10, "Ketua Rayon 33", "Rayon 33", "081234567891" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDZeJ+D82UqkIOZuPJB7vu8G6nV6sBcNbiwh7GAJAStWPqQjBfmsRpJ/qiodH8YA+g==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEErHuvUWPNNBvc6eNbTqFj32F/+ILcK2k/ChJsvGLzTjtdllXEvFWzdSxQkfFBHZgg==");

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "KetuaRayon", "Nama" },
                values: new object[] { "Ketua Rayon I", "Rayon I" });

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KetuaRayon", "Nama" },
                values: new object[] { "Ketua Rayon II", "Rayon II" });

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "KetuaRayon", "Nama" },
                values: new object[] { "Ketua Rayon III", "Rayon III" });
        }
    }
}
