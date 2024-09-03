using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class HapusRangkumanPembedarahan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                value: "AQAAAAIAAYagAAAAEJc2W92AbKb3KwWfqAAFEMvcEvN+YHWLOgpYzbr5YnIXzHcupcvE6YqyEXeVqKcAbg==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFdDMbazdNfJyno9uZYxhfFjlUs3LLEPhgpTnMD5bHGYpM+uAoJxkokwcXQaMMKp3Q==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                value: "AQAAAAIAAYagAAAAENSZvTYFUc9PPLSGRzzliUAxrya3iPBNOtSp/Q68WCUxD0ALEUYlHQd074pFCucmvQ==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDICBml07tf7jidgjDzllma970FZtYfO8E028zxP08C2s4xvObZf1ZKaC5gBGN+pGQ==");

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
    }
}
