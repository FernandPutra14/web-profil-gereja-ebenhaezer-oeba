using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PerbaikiFotoPendeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEHswIJbGeYNMC5TZQGu8lKuCfFQPfIXrWDSU5Hf+oNBjGROeqSF4Q993PoS2I705/g==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJqySuiZkxohU388ga/tGTh16TGe61FvRi5Gv12uBafeHuoxkUOadxO0qe8+xL2zxw==");

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
                value: "AQAAAAIAAYagAAAAENSZvTYFUc9PPLSGRzzliUAxrya3iPBNOtSp/Q68WCUxD0ALEUYlHQd074pFCucmvQ==");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDICBml07tf7jidgjDzllma970FZtYfO8E028zxP08C2s4xvObZf1ZKaC5gBGN+pGQ==");

            migrationBuilder.UpdateData(
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                column: "PathFoto",
                value: "wwwroot/img/default_img/generaluser.png");
        }
    }
}
