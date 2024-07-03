using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TambahPendeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pendeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nama = table.Column<string>(type: "text", nullable: false),
                    FotoId = table.Column<int>(type: "integer", nullable: true),
                    FacebookProfileLink = table.Column<string>(type: "text", nullable: true),
                    InstagramProfileLink = table.Column<string>(type: "text", nullable: true),
                    YoutubeProfileLink = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pendeta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pendeta_FotoTable_FotoId",
                        column: x => x.FotoId,
                        principalTable: "FotoTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEIvohYWgv8an2nzRLK0mz1EPOP+0vBPreqC/EzIHYqPecPe2yTFeHz1JQXEFBaPLcw==");

            migrationBuilder.CreateIndex(
                name: "IX_Pendeta_FotoId",
                table: "Pendeta",
                column: "FotoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pendeta");

            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEOh6knBU+aLNNW7gTPmYQiLpojvr6Muexs2B1NuGpGA9a80oXvKeMplAvuKhksL0lw==");
        }
    }
}
