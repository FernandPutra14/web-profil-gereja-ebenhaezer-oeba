using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahIndexTanggalWartaDiWartaJemaat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOh6knBU+aLNNW7gTPmYQiLpojvr6Muexs2B1NuGpGA9a80oXvKeMplAvuKhksL0lw==");

            migrationBuilder.CreateIndex(
                name: "IX_WartaJemaatTable_TanggalWarta",
                table: "WartaJemaatTable",
                column: "TanggalWarta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WartaJemaatTable_TanggalWarta",
                table: "WartaJemaatTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHu2Dx8N8bgIXiKYNUKYWNQYWERHA+KvqAYswWR8tv9vXisAoDV/L4k/n/yH0J9M6A==");
        }
    }
}
