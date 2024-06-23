using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ubahNamaTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PengumumenTable",
                table: "PengumumenTable");

            migrationBuilder.RenameTable(
                name: "PengumumenTable",
                newName: "PengumumanTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PengumumanTable",
                table: "PengumumanTable",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEDyn0bTgcKRehvwG6uFe5ECh1caxJDVGpjGMwfv+mP5lPdVdD6Fezlr24BuyH5x4Lw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PengumumanTable",
                table: "PengumumanTable");

            migrationBuilder.RenameTable(
                name: "PengumumanTable",
                newName: "PengumumenTable");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PengumumenTable",
                table: "PengumumenTable",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEP9Jkku7w3fpizd144MLLx4AiLL8LVfW+OtDX2+7vn7sVjCRScHH1Prv0jH+i574Ww==");
        }
    }
}
