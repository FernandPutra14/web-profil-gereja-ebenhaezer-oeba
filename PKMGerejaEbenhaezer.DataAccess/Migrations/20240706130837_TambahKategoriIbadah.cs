using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahKategoriIbadah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KategoriIbadahTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama = table.Column<string>(type: "text", nullable: false),
                    Durasi = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KategoriIbadahTable", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENYeKbHcQeVotTea0vOgixhrhl6DeXJ4gHHS5Lo9yvok6IxozJ0pOTfHH+V1pHQSow==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEE9SxGWFMNhGjEkxLrb+ypG0PNwXltCYgy3dqTtmP6T3iDq6gGHCBieDF3fktTeSRQ==");

            migrationBuilder.InsertData(
                table: "KategoriIbadahTable",
                columns: new[] { "Id", "Durasi", "Nama" },
                values: new object[] { 1, new TimeSpan(0, 2, 0, 0, 0), "Kebaktian Umum Pagi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KategoriIbadahTable");

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
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEM0TqTyg5NBgHstMi155CyZAW4cUx4XyogXZvssPBM660eQGun8oUemA7ggZJSGjAQ==");
        }
    }
}
