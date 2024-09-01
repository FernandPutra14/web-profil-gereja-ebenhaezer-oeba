using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahRangkumanPembedaharaanDiWarta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Penerimaan",
                table: "WartaJemaatTable",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Pengeluaran",
                table: "WartaJemaatTable",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SaldoKas",
                table: "WartaJemaatTable",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMZE2R1EJaCt093iDWH/UDYUGy8RhaWrNyNnspOUkh3nTxIHueDTauCi/xlE1pb6vg==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFhgcmbE+9k+/AnePTu/pvA9VMcrm6g/P6bkySfLF+OH5SKVuLUnY+nzW7r1WqPuDA==");

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Penerimaan", "Pengeluaran", "SaldoKas" },
                values: new object[] { 100000000.0, 100000000.0, 300000000.0 });

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Penerimaan", "Pengeluaran", "SaldoKas" },
                values: new object[] { 100000000.0, 50000000.0, 350000000.0 });

            migrationBuilder.UpdateData(
                table: "WartaJemaatTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Penerimaan", "Pengeluaran", "SaldoKas" },
                values: new object[] { 200000000.0, 100000000.0, 400000000.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Penerimaan",
                table: "WartaJemaatTable");

            migrationBuilder.DropColumn(
                name: "Pengeluaran",
                table: "WartaJemaatTable");

            migrationBuilder.DropColumn(
                name: "SaldoKas",
                table: "WartaJemaatTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENCiedELQsD/4CmMEPzvf6FaWDnG/jloj7ilwaP6CzLL9Yjj3TAsdhVWB+rTfDfEtw==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELY9i1MfpWY2rY/oeY1vxZgSpQgJKeORXY5m9wIe6XMQsxyro91K7Z2azuQzSA+FTA==");
        }
    }
}
