using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahPathFotoSmallMediumLarge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PathFotoKompresi",
                table: "FotoTable",
                newName: "PathFotoSmall");

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
                keyValue: 1,
                columns: new[] { "PathFotoLarge", "PathFotoMedium" },
                values: new object[] { "wwwroot/img/pengumuman/natall.jpg", "wwwroot/img/pengumuman/natall.jpg" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathFotoLarge",
                table: "FotoTable");

            migrationBuilder.DropColumn(
                name: "PathFotoMedium",
                table: "FotoTable");

            migrationBuilder.RenameColumn(
                name: "PathFotoSmall",
                table: "FotoTable",
                newName: "PathFotoKompresi");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKXTd0++0Z4llUNMHOalAMk7WAHrYv327seSfXtWmROKsh+z+0or+mA5FVe6mnYweg==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDuO6AUgZ9yQDmnEuECs1B9nNIeQH8FGPY+vV8jY8tU48vnuxgGzgLlLwTVQa5cCKg==");
        }
    }
}
