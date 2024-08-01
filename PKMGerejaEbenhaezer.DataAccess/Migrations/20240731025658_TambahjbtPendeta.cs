using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahjbtPendeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "jabatan",
                table: "PendetaTable",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJ8+93b30IlcBClHysa2R5t46Kz9BEcPbIklA8hrry0/gbadnCs8VW/UwbCaCQbuGw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEEMk4HeZ+90BHTxm59wv1iSAkFFdlE0Z9BH/vFD9q9dd75a/SAdmHNYTdjLIF4wDsw==");

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "jabatan",
                value: null);

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "jabatan",
                value: null);

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "jabatan",
                value: null);

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "jabatan",
                value: null);

            migrationBuilder.UpdateData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "jabatan",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jabatan",
                table: "PendetaTable");

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
        }
    }
}
