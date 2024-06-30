using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahDokumenDiPengumuman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HaveDocuments",
                table: "PengumumanTable",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PathPDF",
                table: "PengumumanTable",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEI70/mea6SClswMobVwq+eRcqm/ROalk1ahYTXQfb6gjPPmY0/gW40cuNabo7grFVQ==");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HaveDocuments", "PathPDF" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "HaveDocuments", "PathPDF" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HaveDocuments", "PathPDF" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "HaveDocuments", "PathPDF" },
                values: new object[] { false, null });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "HaveDocuments", "PathPDF" },
                values: new object[] { false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaveDocuments",
                table: "PengumumanTable");

            migrationBuilder.DropColumn(
                name: "PathPDF",
                table: "PengumumanTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEOZFQhgBrAPG3NWeYli/x1J8QVxjTVja4CYDNobGMWXydn9myvzkl/pRs+kaMcFegg==");
        }
    }
}
