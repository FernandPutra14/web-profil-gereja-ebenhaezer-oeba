using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahKolomRoleDiAppUserDanTambahSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AppUserTable",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Role" },
                values: new object[] { "AQAAAAIAAYagAAAAED8SjIC3zIZNRDEfFOyMQItGyy9z3Js+dHxUuRKhYBEid40lS8h12wXnLDwvG5aueA==", "Admin" });

            migrationBuilder.InsertData(
                table: "AppUserTable",
                columns: new[] { "Id", "PasswordHash", "Role", "UserName" },
                values: new object[] { 2, "AQAAAAIAAYagAAAAEH/+BB5KUfPY5zDqsvmwNhCA8Zm1BHrO2UwNSYs8fgQWcuyPImU01YWH8Fv3sxAX5g==", "Super Admin", "super" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AppUserTable");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEAhPXpdwb31iCHWi50OGMQkK1HQ7Z/7Qe+KTOGyGr6p7FG0u1ghY9j+zZs93jTtOkQ==");
        }
    }
}
