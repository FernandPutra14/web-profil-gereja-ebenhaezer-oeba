using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PengumumenTable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Judul = table.Column<string>(type: "text", nullable: false),
                    Keterangan = table.Column<string>(type: "text", nullable: false),
                    PathFoto = table.Column<string>(type: "text", nullable: false),
                    Tanggal = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PengumumenTable", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUserTable",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[] { 1, "AQAAAAIAAYagAAAAEP9Jkku7w3fpizd144MLLx4AiLL8LVfW+OtDX2+7vn7sVjCRScHH1Prv0jH+i574Ww==", "admin" });

            migrationBuilder.InsertData(
                table: "PengumumenTable",
                columns: new[] { "Id", "Judul", "Keterangan", "PathFoto", "Tanggal" },
                values: new object[,]
                {
                    { 1, "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg", new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg", new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg", new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg", new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg", new DateTime(2024, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTable_UserName",
                table: "AppUserTable",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserTable");

            migrationBuilder.DropTable(
                name: "PengumumenTable");
        }
    }
}
