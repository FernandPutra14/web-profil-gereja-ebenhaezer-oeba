using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahWartaJemaat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WartaJemaatTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TanggalWarta = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DocumentLink = table.Column<string>(type: "text", nullable: false),
                    TanggalDiBuat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TanggalDiUbah = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PembuatId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WartaJemaatTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WartaJemaatTable_AppUserTable_PembuatId",
                        column: x => x.PembuatId,
                        principalTable: "AppUserTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHu2Dx8N8bgIXiKYNUKYWNQYWERHA+KvqAYswWR8tv9vXisAoDV/L4k/n/yH0J9M6A==");

            migrationBuilder.InsertData(
                table: "WartaJemaatTable",
                columns: new[] { "Id", "DocumentLink", "PembuatId", "TanggalDiBuat", "TanggalDiUbah", "TanggalWarta" },
                values: new object[,]
                {
                    { 1, "https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing", 1, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing", 1, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing", 1, new DateTime(2024, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2024, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WartaJemaatTable_PembuatId",
                table: "WartaJemaatTable",
                column: "PembuatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WartaJemaatTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAXmx1SQsRVEzljlHjyVliLmR6bzZpWqb+wowIWZRZIUSoBgp/4LQs5xY9Ar4w8FYA==");
        }
    }
}
