using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahNamaKolomPengumuman : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HaveDocuments",
                table: "PengumumanTable",
                newName: "HaveDocument");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEEqLRg2tWB6vaE+Nst1tWs1YadhoQ7XBxeAIiy44NC69037XeqywQRmbuPbsUAVfKQ==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HaveDocument",
                table: "PengumumanTable",
                newName: "HaveDocuments");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEI70/mea6SClswMobVwq+eRcqm/ROalk1ahYTXQfb6gjPPmY0/gW40cuNabo7grFVQ==");
        }
    }
}
