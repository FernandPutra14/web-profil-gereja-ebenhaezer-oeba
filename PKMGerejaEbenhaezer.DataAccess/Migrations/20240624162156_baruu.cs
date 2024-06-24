using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class baruu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEOpDqXxWUn2lcmbrUFsTWlMaLPZ8ibXxv5VkY71THaXAoRyE6rPrr+idgIQgNXgR2A==");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PathFoto",
                value: "\\img\\pengumuman\\natall.jpg");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PathFoto",
                value: "\\img\\pengumuman\\rapatt.jpg");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "PathFoto",
                value: "\\img\\pengumuman\\tripp.jpg");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "PathFoto",
                value: "\\img\\pengumuman\\donasii.jpg");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "PathFoto",
                value: "\\img\\pengumuman\\pelayanann.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAECY1qsHpPnuj+FLpx8sQVxk3zf1QQ9zPXaoLB+K7bZugaKZ1eP3mtXcg1JMaW0z16Q==");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PathFoto",
                value: "\\img\\pengumuman\\natal.jpg");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PathFoto",
                value: "\\img\\pengumuman\\rapat.jpg");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "PathFoto",
                value: "\\img\\pengumuman\\trip.jpg");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 4,
                column: "PathFoto",
                value: "\\img\\pengumuman\\donasi.jpg");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 5,
                column: "PathFoto",
                value: "\\img\\pengumuman\\pelayanan.jpg");
        }
    }
}
