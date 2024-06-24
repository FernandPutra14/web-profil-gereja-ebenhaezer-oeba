using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahRayon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RayonTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama = table.Column<string>(type: "text", nullable: false),
                    KetuaRayon = table.Column<string>(type: "text", nullable: false),
                    FotoKetua = table.Column<byte[]>(type: "bytea", nullable: false),
                    JumlahLakiLaki = table.Column<int>(type: "integer", nullable: false),
                    JumlahPerempuan = table.Column<int>(type: "integer", nullable: false),
                    JumlahAnak = table.Column<int>(type: "integer", nullable: false),
                    JumlahRemaja = table.Column<int>(type: "integer", nullable: false),
                    JumlahPemuda = table.Column<int>(type: "integer", nullable: false),
                    JumlahDewasa = table.Column<int>(type: "integer", nullable: false),
                    JumlahLansia = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RayonTable", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEGNQJmKL5RKdArT9DWkC5DpAm31sVmFy8waliSZW3d8W3AEAW25ZCoRdm0HXtG6/nw==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RayonTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEDyn0bTgcKRehvwG6uFe5ECh1caxJDVGpjGMwfv+mP5lPdVdD6Fezlr24BuyH5x4Lw==");
        }
    }
}
