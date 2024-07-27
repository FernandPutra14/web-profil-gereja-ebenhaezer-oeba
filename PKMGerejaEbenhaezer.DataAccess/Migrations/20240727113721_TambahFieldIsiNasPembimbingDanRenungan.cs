using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahFieldIsiNasPembimbingDanRenungan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsiNasPembimbing",
                table: "IbadahTable",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IsiRenungan",
                table: "IbadahTable",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDZeJ+D82UqkIOZuPJB7vu8G6nV6sBcNbiwh7GAJAStWPqQjBfmsRpJ/qiodH8YA+g==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEErHuvUWPNNBvc6eNbTqFj32F/+ILcK2k/ChJsvGLzTjtdllXEvFWzdSxQkfFBHZgg==");

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsiNasPembimbing", "IsiRenungan" },
                values: new object[] { "Isi Naspembimbing", "Isi Renungan" });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsiNasPembimbing", "IsiRenungan" },
                values: new object[] { "Isi Naspembimbing", "Isi Renungan" });

            migrationBuilder.UpdateData(
                table: "IbadahTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsiNasPembimbing", "IsiRenungan" },
                values: new object[] { "Isi Naspembimbing", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsiNasPembimbing",
                table: "IbadahTable");

            migrationBuilder.DropColumn(
                name: "IsiRenungan",
                table: "IbadahTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENhGrzSbpZFo/RHm6DTbG/68wQWQ7NQGP6NKOcTghK4BGMrKz9ba25Mdpvgit03zUQ==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKxB44fC+tDG7R3myA90b9LjPNvzH8CQi7eiGvsbtf4RXyvMMsW/8HA+rOBdpf+XwA==");
        }
    }
}
