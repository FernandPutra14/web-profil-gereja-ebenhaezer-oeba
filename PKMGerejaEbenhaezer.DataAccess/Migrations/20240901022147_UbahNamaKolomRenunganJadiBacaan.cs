using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahNamaKolomRenunganJadiBacaan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Renungan",
                table: "IbadahTable",
                newName: "IsiBacaan");

            migrationBuilder.RenameColumn(
                name: "NasPembimbing",
                table: "IbadahTable",
                newName: "NatsPembimbing");

            migrationBuilder.RenameColumn(
                name: "IsiRenungan",
                table: "IbadahTable",
                newName: "Bacaan");

            migrationBuilder.RenameColumn(
                name: "IsiNasPembimbing",
                table: "IbadahTable",
                newName: "IsiNatsPembimbing");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFlNEXHeI6u9qikdoYDjZ/X4TH6Zh6QcBHrMNxmSKsD1ZXsResyh3lvibdRwuUvTOg==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGqjq2XzsJdnCvAfYuRd4pkOXght/exUkr2w+vzS/asygHz0pQWnSL2PunzUHQoEVw==");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Bacaan", "IsiBacaan", "IsiNatsPembimbing", "NatsPembimbing" },
                values: new object[] { "Markus 3:4-5", "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b>Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.", "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.", "Mazmur 12:2" });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Bacaan", "IsiBacaan", "IsiNatsPembimbing", "NatsPembimbing" },
                values: new object[] { "Markus 3:4-5", "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b>Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.", "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.", "Mazmur 12:2" });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsiNatsPembimbing", "NatsPembimbing" },
                values: new object[] { "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.", "Mazmur 12:2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NatsPembimbing",
                table: "IbadahTable",
                newName: "NasPembimbing");

            migrationBuilder.RenameColumn(
                name: "IsiNatsPembimbing",
                table: "IbadahTable",
                newName: "IsiNasPembimbing");

            migrationBuilder.RenameColumn(
                name: "IsiBacaan",
                table: "IbadahTable",
                newName: "Renungan");

            migrationBuilder.RenameColumn(
                name: "Bacaan",
                table: "IbadahTable",
                newName: "IsiRenungan");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHusPYXs4CHcwYanev+UOiwAd8kLyRkxfudFts02wsQxaLwfmRx1Iq6pkAFv7BcKhw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELD4MACBNZqQc5/Ur3IKy5D66OtEPLzNRQIxjyEr7DntKAIKBZsYanLdFjrViTy3iA==");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsiNasPembimbing", "IsiRenungan", "NasPembimbing", "Renungan" },
                values: new object[] { "Isi Naspembimbing", "Isi Renungan", "Mazmur 12:15", "Markus 3:4,15" });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsiNasPembimbing", "IsiRenungan", "NasPembimbing", "Renungan" },
                values: new object[] { "Isi Naspembimbing", "Isi Renungan", "Mazmur 12:15", "Markus 3:4-15" });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsiNasPembimbing", "NasPembimbing" },
                values: new object[] { "Isi Naspembimbing", "Matius 3:16" });
        }
    }
}
