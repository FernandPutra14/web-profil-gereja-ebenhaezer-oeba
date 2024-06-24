using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class HapusTabelRayon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RayonTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEEkMOoSrttL4o0iYCmtq3LvNy8jYly3Oe1JNjkeMaSoAw7xAPNhQpRp56z70Qp7mWA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RayonTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FotoKetua = table.Column<byte[]>(type: "bytea", nullable: false),
                    JumlahAnak = table.Column<int>(type: "integer", nullable: false),
                    JumlahDewasa = table.Column<int>(type: "integer", nullable: false),
                    JumlahLakiLaki = table.Column<int>(type: "integer", nullable: false),
                    JumlahLansia = table.Column<int>(type: "integer", nullable: false),
                    JumlahPemuda = table.Column<int>(type: "integer", nullable: false),
                    JumlahPerempuan = table.Column<int>(type: "integer", nullable: false),
                    JumlahRemaja = table.Column<int>(type: "integer", nullable: false),
                    KetuaRayon = table.Column<string>(type: "text", nullable: false),
                    Nama = table.Column<string>(type: "text", nullable: false)
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
                value: "AQAAAAIAAYagAAAAELS2ozkFOogGJF+u6o+DDaynwNrmQKe1yk1BkVWWcpQPruRYRf+GH+9cr5Ey1XVNow==");
        }
    }
}
