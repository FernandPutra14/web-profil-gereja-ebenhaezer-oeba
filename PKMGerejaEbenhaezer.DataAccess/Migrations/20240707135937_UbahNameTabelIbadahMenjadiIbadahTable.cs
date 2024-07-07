using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahNameTabelIbadahMenjadiIbadahTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ibadah_KategoriIbadahTable_KategoriIbadahId",
                table: "Ibadah");

            migrationBuilder.DropForeignKey(
                name: "FK_Ibadah_PendetaTable_PendetaId",
                table: "Ibadah");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ibadah",
                table: "Ibadah");

            migrationBuilder.RenameTable(
                name: "Ibadah",
                newName: "IbadahTable");

            migrationBuilder.RenameIndex(
                name: "IX_Ibadah_PendetaId",
                table: "IbadahTable",
                newName: "IX_IbadahTable_PendetaId");

            migrationBuilder.RenameIndex(
                name: "IX_Ibadah_KategoriIbadahId",
                table: "IbadahTable",
                newName: "IX_IbadahTable_KategoriIbadahId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IbadahTable",
                table: "IbadahTable",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFmE+1Gc+hw57HUzZNfk2k0mF74F765O94z9cNWMjOky4KP/+TJuBVTX1r2hTICunw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEE+1BOcV5GJFHTNGUhASdjhy9O8qFUy5MCH1idKZIYEFIHMszsQNQ79RgFZEDiOjEg==");

            migrationBuilder.AddForeignKey(
                name: "FK_IbadahTable_KategoriIbadahTable_KategoriIbadahId",
                table: "IbadahTable",
                column: "KategoriIbadahId",
                principalTable: "KategoriIbadahTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_IbadahTable_PendetaTable_PendetaId",
                table: "IbadahTable",
                column: "PendetaId",
                principalTable: "PendetaTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IbadahTable_KategoriIbadahTable_KategoriIbadahId",
                table: "IbadahTable");

            migrationBuilder.DropForeignKey(
                name: "FK_IbadahTable_PendetaTable_PendetaId",
                table: "IbadahTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IbadahTable",
                table: "IbadahTable");

            migrationBuilder.RenameTable(
                name: "IbadahTable",
                newName: "Ibadah");

            migrationBuilder.RenameIndex(
                name: "IX_IbadahTable_PendetaId",
                table: "Ibadah",
                newName: "IX_Ibadah_PendetaId");

            migrationBuilder.RenameIndex(
                name: "IX_IbadahTable_KategoriIbadahId",
                table: "Ibadah",
                newName: "IX_Ibadah_KategoriIbadahId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ibadah",
                table: "Ibadah",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ibadah_KategoriIbadahTable_KategoriIbadahId",
                table: "Ibadah",
                column: "KategoriIbadahId",
                principalTable: "KategoriIbadahTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Ibadah_PendetaTable_PendetaId",
                table: "Ibadah",
                column: "PendetaId",
                principalTable: "PendetaTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
