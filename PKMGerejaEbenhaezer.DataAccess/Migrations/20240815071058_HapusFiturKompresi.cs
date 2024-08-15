using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class HapusFiturKompresi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathFotoLarge",
                table: "FotoTable");

            migrationBuilder.DropColumn(
                name: "PathFotoMedium",
                table: "FotoTable");

            migrationBuilder.DropColumn(
                name: "PathFotoSmall",
                table: "FotoTable");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathFotoLarge",
                table: "FotoTable",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PathFotoMedium",
                table: "FotoTable",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PathFotoSmall",
                table: "FotoTable",
                type: "text",
                nullable: false,
                defaultValue: "");

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
                keyValue: 1,
                columns: new[] { "PathFotoLarge", "PathFotoMedium", "PathFotoSmall" },
                values: new object[] { "wwwroot/img/pengumuman/natall.jpg", "wwwroot/img/pengumuman/natall.jpg", "wwwroot/img/pengumuman/natall.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PathFotoLarge", "PathFotoMedium", "PathFotoSmall" },
                values: new object[] { "wwwroot/img/pengumuman/rapatt.jpg", "wwwroot/img/pengumuman/rapatt.jpg", "wwwroot/img/pengumuman/rapatt.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PathFotoLarge", "PathFotoMedium", "PathFotoSmall" },
                values: new object[] { "wwwroot/img/pengumuman/tripp.jpg", "wwwroot/img/pengumuman/tripp.jpg", "wwwroot/img/pengumuman/tripp.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PathFotoLarge", "PathFotoMedium", "PathFotoSmall" },
                values: new object[] { "wwwroot/img/pengumuman/donasii.jpg", "wwwroot/img/pengumuman/donasii.jpg", "wwwroot/img/pengumuman/donasii.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PathFotoLarge", "PathFotoMedium", "PathFotoSmall" },
                values: new object[] { "wwwroot/img/pengumuman/pelayanann.jpg", "wwwroot/img/pengumuman/pelayanann.jpg", "wwwroot/img/pengumuman/pelayanann.jpg" });

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PathFotoLarge", "PathFotoMedium", "PathFotoSmall" },
                values: new object[] { "wwwroot/img/generaluser.png", "wwwroot/img/generaluser.png", "wwwroot/img/generaluser.png" });
        }
    }
}
