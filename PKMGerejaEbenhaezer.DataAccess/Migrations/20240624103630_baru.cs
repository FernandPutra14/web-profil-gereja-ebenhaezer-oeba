using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PKMGerejaEbenhaezer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class baru : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAECY1qsHpPnuj+FLpx8sQVxk3zf1QQ9zPXaoLB+K7bZugaKZ1eP3mtXcg1JMaW0z16Q==");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Ibadah Natal Bersama", "Kami mengundang seluruh jemaat untuk hadir dalam Ibadah Natal yang akan diadakan pada hari Minggu, 25 Desember 2024, pukul 10.00 WIB di Gereja. Mari kita rayakan kelahiran Yesus Kristus dengan sukacita dan kebersamaan. Setelah ibadah, akan diadakan acara ramah tamah di aula gereja.", "\\img\\pengumuman\\natal.jpg" });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Rapat Anggota Jemaat Tahunan", "Rapat Anggota Jemaat Tahunan akan dilaksanakan pada hari Sabtu, 27 Juni 2024, pukul 14.00 WIB di aula gereja. Kami mengajak seluruh jemaat untuk hadir dan berpartisipasi dalam membahas laporan tahunan dan rencana kegiatan gereja untuk tahun 2024.", "\\img\\pengumuman\\rapat.jpg" });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Retret Pemuda Gereja", "Kami mengundang para pemuda gereja untuk mengikuti Retret Pemuda yang akan diadakan pada tanggal 15-17 Juli 2024 di Wisma Retreat Agape. Tema retret kali ini adalah \"Membangun Iman yang Kuat di Era Digital\". Pendaftaran dapat dilakukan melalui sekretariat gereja hingga 1 Juli 2024.", "\\img\\pengumuman\\trip.jpg" });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Penggalangan Dana untuk Renovasi Gereja", "Gereja kita akan mengadakan penggalangan dana untuk renovasi bangunan gereja yang direncanakan mulai bulan Juni 2024. Kami mengajak seluruh jemaat untuk berpartisipasi dalam acara ini pada hari Minggu, 5 Juli 2024, pukul 09.00 WIB setelah kebaktian. Donasi dapat diberikan langsung atau melalui rekening gereja.", "\\img\\pengumuman\\donasi.jpg" });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Pelayanan Sosial Natal", "Dalam rangka menyambut Natal, gereja akan mengadakan kegiatan pelayanan sosial ke Panti Asuhan Kasih Ibu pada hari Sabtu, 23 Desember 2024. Kami mengundang jemaat untuk ikut serta dalam kegiatan ini dengan menyumbangkan pakaian layak pakai, mainan, dan sembako. Barang-barang sumbangan dapat dikumpulkan di kantor gereja hingga 21 Desember 2024.", "\\img\\pengumuman\\pelayanan.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUserTable",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAIAAYagAAAAEIVVhrBNJgKaGkQtf4W3nFdDX74u/uTMOPxy4Ww8jagED6OIHk1VlxJHuMOIP0JoGQ==");

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg" });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg" });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg" });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg" });

            migrationBuilder.UpdateData(
                table: "PengumumanTable",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Judul", "Keterangan", "PathFoto" },
                values: new object[] { "Lionel Messi lebih jago dari Cristiano Ronaldo karena kemampuan dribelnya yang luar biasa dan visi bermain yang sangat tajam.", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Varius quam quisque id diam vel quam elementum pulvinar. Nisi scelerisque eu ultrices vitae. Risus commodo viverra maecenas accumsan lacus vel facilisis. Pharetra diam sit amet nisl. Suscipit adipiscing bibendum est ultricies integer quis auctor elit. Pharetra vel turpis nunc eget lorem dolor. Eros donec ac odio tempor orci. Diam sit amet nisl suscipit adipiscing bibendum est ultricies Lorem ipsum dolor sit amet.", "\\img\\gereja1.jpg" });
        }
    }
}
