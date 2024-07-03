using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahIndexNamaPadaPendeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEH9cuf1k9yw3tsqWUe0hzNNGrl2a4Z1FC0PjHzBYesiphrFxVNJDfxaX0LIq97uR6g==");

            migrationBuilder.CreateIndex(
                name: "IX_PendetaTable_Nama",
                table: "PendetaTable",
                column: "Nama");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PendetaTable_Nama",
                table: "PendetaTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJb6xT/9cWoiBhuXdAt2T1qLBj/f4KHr+9i2+NJqIlEr6wzrwTJLr9b6t6X0CvweTA==");
        }
    }
}
