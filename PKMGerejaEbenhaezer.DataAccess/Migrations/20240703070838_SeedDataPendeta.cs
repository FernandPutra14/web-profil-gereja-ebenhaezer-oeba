using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataPendeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pendeta_FotoTable_FotoId",
                table: "Pendeta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pendeta",
                table: "Pendeta");

            migrationBuilder.RenameTable(
                name: "Pendeta",
                newName: "PendetaTable");

            migrationBuilder.RenameIndex(
                name: "IX_Pendeta_FotoId",
                table: "PendetaTable",
                newName: "IX_PendetaTable_FotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PendetaTable",
                table: "PendetaTable",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEJb6xT/9cWoiBhuXdAt2T1qLBj/f4KHr+9i2+NJqIlEr6wzrwTJLr9b6t6X0CvweTA==");

            migrationBuilder.InsertData(
                table: "PendetaTable",
                columns: new[] { "Id", "FacebookProfileLink", "FotoId", "InstagramProfileLink", "Nama", "YoutubeProfileLink" },
                values: new object[,]
                {
                    { 1, null, 6, null, "Pdt. Elen Th. Bailaen-Manafe, S.Si (Teol)", null },
                    { 2, null, 6, null, "Pdt. Aleida Y. Salean Sola, S.Th, M.Hum", null },
                    { 3, null, 6, null, "Pdt. Amelia Retha-Siokain, S.Th", null },
                    { 4, null, 6, null, "Pdt. Tera D. Klaping, M.Th", null },
                    { 5, null, 6, null, "Ita Tassi Adoe, S.Th.", null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PendetaTable_FotoTable_FotoId",
                table: "PendetaTable",
                column: "FotoId",
                principalTable: "FotoTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendetaTable_FotoTable_FotoId",
                table: "PendetaTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PendetaTable",
                table: "PendetaTable");

            migrationBuilder.DeleteData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PendetaTable",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.RenameTable(
                name: "PendetaTable",
                newName: "Pendeta");

            migrationBuilder.RenameIndex(
                name: "IX_PendetaTable_FotoId",
                table: "Pendeta",
                newName: "IX_Pendeta_FotoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pendeta",
                table: "Pendeta",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIvohYWgv8an2nzRLK0mz1EPOP+0vBPreqC/EzIHYqPecPe2yTFeHz1JQXEFBaPLcw==");

            migrationBuilder.AddForeignKey(
                name: "FK_Pendeta_FotoTable_FotoId",
                table: "Pendeta",
                column: "FotoId",
                principalTable: "FotoTable",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
