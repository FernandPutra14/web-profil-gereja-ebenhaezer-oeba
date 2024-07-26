using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahKolomNoWaDiRayon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NoWa",
                table: "RayonTable",
                type: "text",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "NoWa",
                value: "081234567891");

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "NoWa",
                value: "081234567891");

            migrationBuilder.UpdateData(
                table: "RayonTable",
                keyColumn: "Id",
                keyValue: 3,
                column: "NoWa",
                value: "081234567891");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoWa",
                table: "RayonTable");

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
