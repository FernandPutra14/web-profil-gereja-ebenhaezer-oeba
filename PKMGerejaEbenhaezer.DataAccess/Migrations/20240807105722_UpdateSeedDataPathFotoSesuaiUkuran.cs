using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataPathFotoSesuaiUkuran : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELJTaJFkaWib8r8EausrbNpBhJqKgCdkwN+DvYkA/h7FUOf9/MrMeBV7iqnnb8wxlQ==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEETTe/3IT6uHTMGqC91FnZD4cgqKUPQsFfR6GcGtWwmuemyBI8HKZbtQz/QpsWBdEA==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/rapatt.jpg", "wwwroot/img/pengumuman/rapatt.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/tripp.jpg", "wwwroot/img/pengumuman/tripp.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/donasii.jpg", "wwwroot/img/pengumuman/donasii.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/pelayanann.jpg", "wwwroot/img/pengumuman/pelayanann.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/generaluser.png", "wwwroot/img/generaluser.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAaPXuJHksi/2ecjkd5nhKTbxSu3pYIei8FI7Q8NW301ukfKXKpeiJ4IPXapeaPXwg==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEL41Ru8q10GIWJ1/ppnefKNfqqPMsnmeE/fV55Sgysd3HFkzZex9oDJVWsXHFguglg==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/natall.jpg", "wwwroot/img/pengumuman/natall.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/natall.jpg", "wwwroot/img/pengumuman/natall.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/natall.jpg", "wwwroot/img/pengumuman/natall.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/natall.jpg", "wwwroot/img/pengumuman/natall.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/natall.jpg", "wwwroot/img/pengumuman/natall.jpg" });
        }
    }
}
