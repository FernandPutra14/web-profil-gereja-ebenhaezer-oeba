using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataIsiBacaanIbadah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                column: "IsiBacaan",
                value: "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b> Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsiBacaan",
                value: "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b> Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                column: "IsiBacaan",
                value: "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b>Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsiBacaan",
                value: "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b>Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.");
        }
    }
}
