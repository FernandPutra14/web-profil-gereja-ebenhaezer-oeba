using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedDataPendeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENpikUFqSLoJMmA9aEKfD7B7ReeFSLOdbab10Z+C9W0LzTr0RpKoXjo2uYjOkzTI9w==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEL/SarlaxAmALp0S9Vbjhv/DnNhNNDDFEv9gClEooPeSiBqL+LBiLV5mjzKs2wrR+g==");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nama",
                value: "Pendeta 1");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nama",
                value: "Pendeta 2");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nama",
                value: "Pendeta 3");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "Nama",
                value: "Pendeta 4");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "Nama",
                value: "Pendeta 5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIhoMqNTzf43/qX1OaAA3mwrqMCLN4/w9awv7MmJzCMVxh+4RTsQYY6MAYcuiI+SCw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBUbTOjkFaIciuZyc6VE2zdyaOLmQO8t3Po14eOYORPqh27TSv9sZfvYOdRiOgeJuQ==");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nama",
                value: "Pdt. Elen Th. Bailaen-Manafe, S.Si (Teol)");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nama",
                value: "Pdt. Aleida Y. Salean Sola, S.Th, M.Hum");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nama",
                value: "Pdt. Amelia Retha-Siokain, S.Th");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "Nama",
                value: "Pdt. Tera D. Klaping, M.Th");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "Nama",
                value: "Ita Tassi Adoe, S.Th.");
        }
    }
}
