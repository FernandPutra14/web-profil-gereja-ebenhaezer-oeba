using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.DataAccess
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder SeedingData(this ModelBuilder modelBuilder)
        {
            var daftarUser = new AppUser[]
            {
                new AppUser
                {
                    Id = 1,
                    UserName = "admin",
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "admin"),
                    Role = AppUserRoles.Admin,
                    LastChanged = new DateTime(2024, 07, 06),
                },
                new AppUser
                {
                    Id = 2,
                    UserName = "super",
                    PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "super"),
                    Role = AppUserRoles.SuperAdmin,
                    LastChanged = new DateTime(2024, 07, 06),
                },
            };

            modelBuilder.Entity<AppUser>().HasData(daftarUser);

            var daftarFoto = new[]
            {
                new
                {
                    Id = 1,
                    PathFoto = "wwwroot/img/pengumuman/natall.jpg",
                    PathFotoKompresi = "wwwroot/img/pengumuman/natall.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 2,
                    PathFoto = "wwwroot/img/pengumuman/rapatt.jpg",
                    PathFotoKompresi = "wwwroot/img/pengumuman/rapatt.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 3,
                    PathFoto = "wwwroot/img/pengumuman/tripp.jpg",
                    PathFotoKompresi = "wwwroot/img/pengumuman/tripp.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 4,
                    PathFoto = "wwwroot/img/pengumuman/donasii.jpg",
                    PathFotoKompresi = "wwwroot/img/pengumuman/donasii.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 5,
                    PathFoto = "wwwroot/img/pengumuman/pelayanann.jpg",
                    PathFotoKompresi = "wwwroot/img/pengumuman/pelayanann.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 6,
                    PathFoto = @"wwwroot/img/generaluser.png",
                    PathFotoKompresi = @"wwwroot/img/generaluser.png",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id
                },
            };

            modelBuilder.Entity<Foto>().HasData(daftarFoto);

            modelBuilder.Entity<Pengumuman>().HasData(
                new
                {
                    Id = 1,
                    Judul = "Ibadah Natal Bersama",
                    Isi = "Kami mengundang seluruh jemaat untuk hadir dalam Ibadah Natal yang akan diadakan pada hari Minggu, 25 Desember 2024, pukul 10.00 WIB di Gereja. Mari kita rayakan kelahiran Yesus Kristus dengan sukacita dan kebersamaan. Setelah ibadah, akan diadakan acara ramah tamah di aula gereja.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[0].Id,
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 2,
                    Judul = "Rapat Anggota Jemaat Tahunan",
                    Isi = "Rapat Anggota Jemaat Tahunan akan dilaksanakan pada hari Sabtu, 27 Juni 2024, pukul 14.00 WIB di aula gereja. Kami mengajak seluruh jemaat untuk hadir dan berpartisipasi dalam membahas laporan tahunan dan rencana kegiatan gereja untuk tahun 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[1].Id,
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 3,
                    Judul = "Retret Pemuda Gereja",
                    Isi = "Kami mengundang para pemuda gereja untuk mengikuti Retret Pemuda yang akan diadakan pada tanggal 15-17 Juli 2024 di Wisma Retreat Agape. Tema retret kali ini adalah \"Membangun Iman yang Kuat di Era Digital\". Pendaftaran dapat dilakukan melalui sekretariat gereja hingga 1 Juli 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[2].Id,
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 4,
                    Judul = "Penggalangan Dana untuk Renovasi Gereja",
                    Isi = "Gereja kita akan mengadakan penggalangan dana untuk renovasi bangunan gereja yang direncanakan mulai bulan Juni 2024. Kami mengajak seluruh jemaat untuk berpartisipasi dalam acara ini pada hari Minggu, 5 Juli 2024, pukul 09.00 WIB setelah kebaktian. Donasi dapat diberikan langsung atau melalui rekening gereja.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[3].Id,
                    PembuatId = daftarUser[0].Id
                },
                new
                {
                    Id = 5,
                    Judul = "Pelayanan Sosial Natal",
                    Isi = "Dalam rangka menyambut Natal, gereja akan mengadakan kegiatan pelayanan sosial ke Panti Asuhan Kasih Ibu pada hari Sabtu, 23 Desember 2024. Kami mengundang jemaat untuk ikut serta dalam kegiatan ini dengan menyumbangkan pakaian layak pakai, mainan, dan sembako. Barang-barang sumbangan dapat dikumpulkan di kantor gereja hingga 21 Desember 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[4].Id,
                    PembuatId = daftarUser[0].Id
                }
            );

            modelBuilder.Entity<Rayon>().HasData(
                new
                {
                    Id = 1,
                    Nama = "Rayon I",
                    FotoKetuaId = daftarFoto[5].Id,
                    KetuaRayon = "Ketua Rayon I",
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                },
                new
                {
                    Id = 2,
                    Nama = "Rayon II",
                    FotoKetuaId = daftarFoto[5].Id,
                    KetuaRayon = "Ketua Rayon II",
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                },
                new
                {
                    Id = 3,
                    Nama = "Rayon III",
                    FotoKetuaId = daftarFoto[5].Id,
                    KetuaRayon = "Ketua Rayon III",
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                }
            );

            modelBuilder.Entity<WartaJemaat>().HasData(
                new
                {
                    Id = 1,
                    TanggalWarta = new DateOnly(2024, 7, 7),
                    DocumentLink = new Uri("https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing"),
                    TanggalDiBuat = new DateTime(2024, 7, 7, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id,
                },
                new
                {
                    Id = 2,
                    TanggalWarta = new DateOnly(2024, 7, 21),
                    DocumentLink = new Uri("https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing"),
                    TanggalDiBuat = new DateTime(2024, 7, 14, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id,
                },
                new
                {
                    Id = 3,
                    TanggalWarta = new DateOnly(2024, 7, 28),
                    DocumentLink = new Uri("https://drive.google.com/file/d/1-O8JJyBhEPPhwjjGKQp9u2gur0gWxuSl/view?usp=sharing"),
                    TanggalDiBuat = new DateTime(2024, 7, 21, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = daftarUser[0].Id,
                }
            );

            modelBuilder.Entity<Pendeta>().HasData(
                new
                {
                    Id = 1,
                    Nama = "Pdt. Elen Th. Bailaen-Manafe, S.Si (Teol)",
                    FotoId = daftarFoto[daftarFoto.Length - 1].Id
                },
                new
                {
                    Id = 2,
                    Nama = "Pdt. Aleida Y. Salean Sola, S.Th, M.Hum",
                    FotoId = daftarFoto[daftarFoto.Length - 1].Id
                },
                new
                {
                    Id = 3,
                    Nama = "Pdt. Amelia Retha-Siokain, S.Th",
                    FotoId = daftarFoto[daftarFoto.Length - 1].Id
                },
                new
                {
                    Id = 4,
                    Nama = "Pdt. Tera D. Klaping, M.Th",
                    FotoId = daftarFoto[daftarFoto.Length - 1].Id
                },
                new
                {
                    Id = 5,
                    Nama = "Ita Tassi Adoe, S.Th.",
                    FotoId = daftarFoto[daftarFoto.Length - 1].Id
                }
            );

            modelBuilder.Entity<KategoriIbadah>().HasData(
                new KategoriIbadah
                {
                    Id = 1,
                    Nama = "Kebaktian Umum",
                    Durasi = new TimeSpan(2, 0, 0)
                },
                new KategoriIbadah
                {
                    Id = 2,
                    Nama = "Perjamuan",
                    Durasi = new TimeSpan(2, 0, 0)
                },
                new KategoriIbadah
                {
                    Id = 3,
                    Nama = "Persiapan Perjamuan",
                    Durasi = new TimeSpan(2, 0, 0)
                }
            );

            modelBuilder.Entity<Ibadah>().HasData(
                new
                {
                    Id = 1,
                    Judul = "Kebaktian Pagi Pertama",
                    Deskripsi = "Kebaktian hari minggu pagi pertama",
                    NasPembimbing = "Mazmur 12:15",
                    TanggalIbadah = new DateTime(2024, 06, 23, 6, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadahId = 1,
                    PendetaId = 1,
                },
                new
                {
                    Id = 2,
                    Judul = "Kebaktian Pagi Kedua",
                    Deskripsi = "Kebaktian hari minggu pagi kedua",
                    NasPembimbing = "Mazmur 12:15",
                    TanggalIbadah = new DateTime(2024, 06, 23, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadahId = 1,
                    PendetaId = 3,
                },
                new
                {
                    Id = 3,
                    Judul = "Perjamuan Bulan Juni",
                    Deskripsi = "Perjamuan Bulan Juni",
                    NasPembimbing = "Matius 3:16",
                    TanggalIbadah = new DateTime(2024, 7, 5, 8, 0, 0),
                    Tempat = "Gedung Gereja Ebenhaezer Oeba",
                    KategoriIbadahId = 2,
                    PendetaId = 2,
                }
            );

            return modelBuilder;
        }
    }
}
