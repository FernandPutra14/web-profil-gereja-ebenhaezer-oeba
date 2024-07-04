using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditIndexAppUserDanPendetaDiNamaMenjadiUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PendetaTable_Nama",
                table: "PendetaTable");

            migrationBuilder.DropIndex(
                name: "IX_AppUserTable_UserName",
                table: "AppUserTable");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "TanggalWarta",
                table: "WartaJemaatTable",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEOxtC5r/5bXfJddpwbPE01tbrmg3FFyD2gE50hpCLY651H45x68uH9nx4VsNESV3w==");

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalWarta",
                value: new DateOnly(2024, 7, 7));

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "TanggalWarta",
                value: new DateOnly(2024, 7, 7));

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "TanggalWarta",
                value: new DateOnly(2024, 7, 7));

            migrationBuilder.CreateIndex(
                name: "IX_PendetaTable_Nama",
                table: "PendetaTable",
                column: "Nama",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTable_UserName",
                table: "AppUserTable",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PendetaTable_Nama",
                table: "PendetaTable");

            migrationBuilder.DropIndex(
                name: "IX_AppUserTable_UserName",
                table: "AppUserTable");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TanggalWarta",
                table: "WartaJemaatTable",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDlSUGtS0u+xK2/Ay4aUUCDh/1Gn4DovXXmN2lWebWwWkeub5Z8/WLwZTNr2UFvXSw==");

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "TanggalWarta",
                value: new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "TanggalWarta",
                value: new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "TanggalWarta",
                value: new DateTime(2024, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_PendetaTable_Nama",
                table: "PendetaTable",
                column: "Nama");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTable_UserName",
                table: "AppUserTable",
                column: "UserName");
        }
    }
}
