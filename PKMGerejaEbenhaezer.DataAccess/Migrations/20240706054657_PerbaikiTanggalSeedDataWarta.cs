using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PerbaikiTanggalSeedDataWarta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKqsL0MsiCLJ4bFhaEwBp/tTNUJdIEPVomaINEIPAijgTRDkBsslKvA1SP35Hvvcjw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "AQAAAAIAAYagAAAAEM0TqTyg5NBgHstMi155CyZAW4cUx4XyogXZvssPBM660eQGun8oUemA7ggZJSGjAQ==", "SuperAdmin" });

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "TanggalWarta",
                value: new DateOnly(2024, 7, 21));

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "TanggalWarta",
                value: new DateOnly(2024, 7, 28));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBtqXD88FuipibsfI6BGsf0EiwvvzozmwvCvKDDJvmli0O4Flol3b7e6oGP/ofAT4Q==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "AQAAAAIAAYagAAAAEGV88RS2VrTHO1uya97wkLooTKf2zULvgBX7Z9jfKnF0ICxTzDCFmkKejkOkKuKvUw==", "Super Admin" });

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
        }
    }
}
