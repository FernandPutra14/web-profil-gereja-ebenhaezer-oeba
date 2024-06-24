using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataRayoPerbaiki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEEbmtTQA3F+QSLVxUPgRYNWzrDXtiS5AwIp9Yz3uvfcYYZeGhwgeqzCWQcxQwFi8Cg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEG6agSyM9+GBTflNDgC/zb5++gPleEsrQ0lk+b0Pzh+utVusXKFX7i1ImbmrBXejfg==");
        }
    }
}
