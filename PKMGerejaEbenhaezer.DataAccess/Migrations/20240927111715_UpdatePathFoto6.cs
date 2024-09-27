using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePathFoto6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHFouf1dj6pyIMrC3cLW/PZixztjbgsPhOScb2m9mQEpAIE+i5sQGiEsn1yyKEtr6Q==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEGQ45lX136E37sNeRw9YOu7To7QnTB9Z184XUdB5zgTMoMNrnTN9F4KoSgjjg/rRZA==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                column: "PathFoto",
                value: "wwwroot/img/default_image/generaluser.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                column: "PathFoto",
                value: "wwwroot/img/default_img/generaluser.png");
        }
    }
}
