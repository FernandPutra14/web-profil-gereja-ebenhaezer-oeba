using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahJadiOnDeleteSetNullKeSemuaRelasi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FotoTable_AppUserTable_PembuatId",
                table: "FotoTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PengumumanTable_AppUserTable_PembuatId",
                table: "PengumumanTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PengumumanTable_FotoTable_FotoId",
                table: "PengumumanTable");

            migrationBuilder.DropForeignKey(
                name: "FK_RayonTable_FotoTable_FotoKetuaId",
                table: "RayonTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAXmx1SQsRVEzljlHjyVliLmR6bzZpWqb+wowIWZRZIUSoBgp/4LQs5xY9Ar4w8FYA==");

            migrationBuilder.AddForeignKey(
                name: "FK_FotoTable_AppUserTable_PembuatId",
                table: "FotoTable",
                column: "PembuatId",
                principalTable: "AppUserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PengumumanTable_AppUserTable_PembuatId",
                table: "PengumumanTable",
                column: "PembuatId",
                principalTable: "AppUserTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_PengumumanTable_FotoTable_FotoId",
                table: "PengumumanTable",
                column: "FotoId",
                principalTable: "FotoTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RayonTable_FotoTable_FotoKetuaId",
                table: "RayonTable",
                column: "FotoKetuaId",
                principalTable: "FotoTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FotoTable_AppUserTable_PembuatId",
                table: "FotoTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PengumumanTable_AppUserTable_PembuatId",
                table: "PengumumanTable");

            migrationBuilder.DropForeignKey(
                name: "FK_PengumumanTable_FotoTable_FotoId",
                table: "PengumumanTable");

            migrationBuilder.DropForeignKey(
                name: "FK_RayonTable_FotoTable_FotoKetuaId",
                table: "RayonTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJ2fAYxjBHYCT6G+SnSqo98PRwSJrZdy2z0oqU8lDKcSCqxQN5BAUzFLDZeeq0NAeg==");

            migrationBuilder.AddForeignKey(
                name: "FK_FotoTable_AppUserTable_PembuatId",
                table: "FotoTable",
                column: "PembuatId",
                principalTable: "AppUserTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PengumumanTable_AppUserTable_PembuatId",
                table: "PengumumanTable",
                column: "PembuatId",
                principalTable: "AppUserTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PengumumanTable_FotoTable_FotoId",
                table: "PengumumanTable",
                column: "FotoId",
                principalTable: "FotoTable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RayonTable_FotoTable_FotoKetuaId",
                table: "RayonTable",
                column: "FotoKetuaId",
                principalTable: "FotoTable",
                principalColumn: "Id");
        }
    }
}
