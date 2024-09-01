using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UbahPathFotoGeneralUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "FotoTable",
                keyColumn: "Id",
                keyValue: 6,
                column: "PathFoto",
                value: "wwwroot/img/generaluser.png");
        }
    }
}
