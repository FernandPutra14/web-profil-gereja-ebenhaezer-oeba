using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahSeedDataIbadah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELgdlZhEW2bxDdGu84/xXn5bWqzaBFPBoZrvdo/YdWcwJ0xLFV7ZSbequgi+imDTAw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELzbGsorfZLX3oQAwA91hi6LNilvp1+GzBkyzwyYXW6h4HA3vOXbRaQE7bI4dtqVEQ==");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Deskripsi", "Judul" },
                values: new object[] { "Kebaktian hari minggu pertama", "Kebaktian I" });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Deskripsi", "Judul", "TanggalIbadah" },
                values: new object[] { "Kebaktian hari minggu kedua", "Kebaktian II", new DateTime(2024, 6, 23, 8, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Bacaan", "Deskripsi", "IsiBacaan", "Judul", "KategoriIbadahId", "PendetaId", "TanggalIbadah" },
                values: new object[] { "Markus 3:4-5", "Kebaktian hari minggu ketiga", "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b> Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.", "Kebaktian III", 1, 3, new DateTime(2024, 6, 23, 16, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "IbadahTable",
                columns: new[] { "Id", "Bacaan", "Deskripsi", "IsiBacaan", "IsiNatsPembimbing", "Judul", "KategoriIbadahId", "NatsPembimbing", "PendetaId", "TanggalIbadah", "Tempat" },
                values: new object[,]
                {
                    { 4, "Markus 3:4-5", "Kebaktian hari minggu keempat", "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b> Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.", "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.", "Kebaktian IV", 1, "Mazmur 12:2", 3, new DateTime(2024, 6, 23, 19, 0, 0, 0, DateTimeKind.Unspecified), "Gedung Gereja Ebenhaezer Oeba" },
                    { 5, null, "Perjamuan Bulan Juni", null, "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.", "Perjamuan Bulan Juni", 2, "Mazmur 12:2", 2, new DateTime(2024, 7, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "Gedung Gereja Ebenhaezer Oeba" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOWZ2IINUaR5HhQsn1d3SKispXQ9Vvvm9oi1wY+y1OLxGMZ3Hebw2lNVQ47RVB0kyQ==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJk45ulRAMU7RsopJpXBV5bVfn1DdjgTzEgiG95CJWpHs6Xu0gBx7U6iLqW23v4nBA==");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Deskripsi", "Judul" },
                values: new object[] { "Kebaktian hari minggu pagi pertama", "Kebaktian Pagi Pertama" });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Deskripsi", "Judul", "TanggalIbadah" },
                values: new object[] { "Kebaktian hari minggu pagi kedua", "Kebaktian Pagi Kedua", new DateTime(2024, 6, 23, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Bacaan", "Deskripsi", "IsiBacaan", "Judul", "KategoriIbadahId", "PendetaId", "TanggalIbadah" },
                values: new object[] { null, "Perjamuan Bulan Juni", null, "Perjamuan Bulan Juni", 2, 2, new DateTime(2024, 7, 5, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
