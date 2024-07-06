using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahIbadahDanSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ibadah",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Judul = table.Column<string>(type: "text", nullable: false),
                    Deskripsi = table.Column<string>(type: "text", nullable: false),
                    NasPembimbing = table.Column<string>(type: "text", nullable: false),
                    Tempat = table.Column<string>(type: "text", nullable: false),
                    TanggalIbadah = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    KategoriIbadahId = table.Column<int>(type: "integer", nullable: true),
                    PendetaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ibadah", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ibadah_KategoriIbadahTable_KategoriIbadahId",
                        column: x => x.KategoriIbadahId,
                        principalTable: "KategoriIbadahTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ibadah_PendetaTable_PendetaId",
                        column: x => x.PendetaId,
                        principalTable: "PendetaTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEF2aKZkmJhKePDoU6Eq4OyGK9UqN8tCki59lfl7dXpd01hu58o3BjTMKQ4m/SglzIw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOZJ6WYQY8lTnAamOC6CWx5anNzzwwzWAyjI7eLPqpTf4LyAvUJFVSpwXXHIH4GL4Q==");

            migrationBuilder.InsertData(
                table: "Ibadah",
                columns: new[] { "Id", "Deskripsi", "Judul", "KategoriIbadahId", "NasPembimbing", "PendetaId", "TanggalIbadah", "Tempat" },
                values: new object[,]
                {
                    { 1, "Kebaktian hari minggu pagi pertama", "Kebaktian Pagi Pertama", 1, "Mazmur 12:15", 1, new DateTime(2024, 6, 23, 6, 0, 0, 0, DateTimeKind.Unspecified), "Gedung Gereja Ebenhaezer Oeba" },
                    { 2, "Kebaktian hari minggu pagi kedua", "Kebaktian Pagi Kedua", 1, "Mazmur 12:15", 3, new DateTime(2024, 6, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), "Gedung Gereja Ebenhaezer Oeba" }
                });

            migrationBuilder.UpdateData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nama",
                value: "Kebaktian Umum");

            migrationBuilder.InsertData(
                table: "KategoriIbadahTable",
                columns: new[] { "Id", "Durasi", "Nama" },
                values: new object[,]
                {
                    { 2, new TimeSpan(0, 2, 0, 0, 0), "Perjamuan" },
                    { 3, new TimeSpan(0, 2, 0, 0, 0), "Persiapan Perjamuan" }
                });

            migrationBuilder.InsertData(
                table: "Ibadah",
                columns: new[] { "Id", "Deskripsi", "Judul", "KategoriIbadahId", "NasPembimbing", "PendetaId", "TanggalIbadah", "Tempat" },
                values: new object[] { 3, "Perjamuan Bulan Juni", "Perjamuan Bulan Juni", 2, "Matius 3:16", 2, new DateTime(2024, 7, 5, 8, 0, 0, 0, DateTimeKind.Unspecified), "Gedung Gereja Ebenhaezer Oeba" });

            migrationBuilder.CreateIndex(
                name: "IX_Ibadah_KategoriIbadahId",
                table: "Ibadah",
                column: "KategoriIbadahId");

            migrationBuilder.CreateIndex(
                name: "IX_Ibadah_PendetaId",
                table: "Ibadah",
                column: "PendetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ibadah");

            migrationBuilder.DeleteData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 3);

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

            migrationBuilder.UpdateData(
                table: "KategoriIbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nama",
                value: "Kebaktian Umum Pagi");
        }
    }
}
