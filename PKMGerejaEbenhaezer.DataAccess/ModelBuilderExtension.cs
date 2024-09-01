using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.ValueObjects;

namespace PKMGerejaEbenhaezer.DataAccess;

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
                TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                PembuatId = daftarUser[0].Id
            },
            new
            {
                Id = 2,
                PathFoto = "wwwroot/img/pengumuman/rapatt.jpg",
                TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                PembuatId = daftarUser[0].Id
            },
            new
            {
                Id = 3,
                PathFoto = "wwwroot/img/pengumuman/tripp.jpg",
                TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                PembuatId = daftarUser[0].Id
            },
            new
            {
                Id = 4,
                PathFoto = "wwwroot/img/pengumuman/donasii.jpg",
                TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                PembuatId = daftarUser[0].Id
            },
            new
            {
                Id = 5,
                PathFoto = "wwwroot/img/pengumuman/pelayanann.jpg",
                TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                PembuatId = daftarUser[0].Id
            },
            new
            {
                Id = 6,
                PathFoto = @"wwwroot/img/generaluser.png",
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
            Enumerable.Range(1, 33).Select(i => new
            {
                Id = i,
                Nama = $"Rayon E{i}",
                FotoKetuaId = daftarFoto[5].Id,
                KetuaRayon = $"Ketua Rayon E{i}",
                NoWa = NoWa.Create("081234567891").Value,
                JumlahLakiLaki = 25,
                JumlahPerempuan = 35,
                JumlahAnak = 10,
                JumlahRemaja = 10,
                JumlahPemuda = 10,
                JumlahDewasa = 15,
                JumlahLansia = 15,
            })
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
                Nama = "Pendeta 1",
                FotoId = daftarFoto[daftarFoto.Length - 1].Id
            },
            new
            {
                Id = 2,
                Nama = "Pendeta 2",
                FotoId = daftarFoto[daftarFoto.Length - 1].Id
            },
            new
            {
                Id = 3,
                Nama = "Pendeta 3",
                FotoId = daftarFoto[daftarFoto.Length - 1].Id
            },
            new
            {
                Id = 4,
                Nama = "Pendeta 4",
                FotoId = daftarFoto[daftarFoto.Length - 1].Id
            },
            new
            {
                Id = 5,
                Nama = "Pendeta 5",
                FotoId = daftarFoto[daftarFoto.Length - 1].Id
            }
        );

        modelBuilder.Entity<KategoriIbadah>().HasData(
            new KategoriIbadah
            {
                Id = 1,
                Nama = "Kebaktian Umum",
                Durasi = new TimeSpan(2, 0, 0),
                Warna = KategoriColors.Warna1,
            },
            new KategoriIbadah
            {
                Id = 2,
                Nama = "Perjamuan",
                Durasi = new TimeSpan(2, 0, 0),
                Warna = KategoriColors.Warna2,
            },
            new KategoriIbadah
            {
                Id = 3,
                Nama = "Persiapan Perjamuan",
                Durasi = new TimeSpan(2, 0, 0),
                Warna = KategoriColors.Warna3 ,
            }
        );

        modelBuilder.Entity<Ibadah>().HasData(
            new
            {
                Id = 1,
                Judul = "Kebaktian I",
                Deskripsi = "Kebaktian hari minggu pertama",
                NatsPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 2 }),
                IsiNatsPembimbing = "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.",
                Bacaan = new AyatAlkitab(Kitab.Markus, 3, new int[] { 4, 5 }),
                IsiBacaan = "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b> Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.",
                TanggalIbadah = new DateTime(2024, 06, 23, 6, 0, 0),
                Tempat = "Gedung Gereja Ebenhaezer Oeba",
                KategoriIbadahId = 1,
                PendetaId = 1,
            },
            new
            {
                Id = 2,
                Judul = "Kebaktian II",
                Deskripsi = "Kebaktian hari minggu kedua",
                NatsPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 2 }),
                IsiNatsPembimbing = "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.",
                Bacaan = new AyatAlkitab(Kitab.Markus, 3, new int[] { 4, 5 }),
                IsiBacaan = "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b> Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.",
                TanggalIbadah = new DateTime(2024, 06, 23, 8, 30, 0),
                Tempat = "Gedung Gereja Ebenhaezer Oeba",
                KategoriIbadahId = 1,
                PendetaId = 3,
            },
            new
            {
                Id = 3,
                Judul = "Kebaktian III",
                Deskripsi = "Kebaktian hari minggu ketiga",
                NatsPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 2 }),
                IsiNatsPembimbing = "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.",
                Bacaan = new AyatAlkitab(Kitab.Markus, 3, new int[] { 4, 5 }),
                IsiBacaan = "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b> Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.",
                TanggalIbadah = new DateTime(2024, 06, 23, 16, 0, 0),
                Tempat = "Gedung Gereja Ebenhaezer Oeba",
                KategoriIbadahId = 1,
                PendetaId = 3,
            },
            new
            {
                Id = 4,
                Judul = "Kebaktian IV",
                Deskripsi = "Kebaktian hari minggu keempat",
                NatsPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 2 }),
                IsiNatsPembimbing = "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.",
                Bacaan = new AyatAlkitab(Kitab.Markus, 3, new int[] { 4, 5 }),
                IsiBacaan = "<b>4</b> Kemudian kata-NYA kepada mereka: \"Manakah yang diperbolehkan pada hari Sabat, berbuat baik atau berbuat jahat, menyelamatkan orang atau membunuh orang?\"</br><b>5</b> Tetapi mereka diam saja. Ia berdukacita karena kedegilan mereka dan dengan marah Ia berkata kepada orang itu: \"Ulurkan tanganmu!\" Dan ia mengelurkan tangannya, maka sembuhlah tangannya itu.",
                TanggalIbadah = new DateTime(2024, 06, 23, 19, 0, 0),
                Tempat = "Gedung Gereja Ebenhaezer Oeba",
                KategoriIbadahId = 1,
                PendetaId = 3,
            },
            new
            {
                Id = 5,
                Judul = "Perjamuan Bulan Juni",
                Deskripsi = "Perjamuan Bulan Juni",
                NatsPembimbing = new AyatAlkitab(Kitab.Mazmur, 12, new int[] { 2 }),
                IsiNatsPembimbing = "<b>2</b> Tolong kiranya, TUHAN, sebab orang saleh telah habis, telah lenyap orang-orang yang setia dari antara anak-anak manusia.",
                TanggalIbadah = new DateTime(2024, 7, 5, 8, 0, 0),
                Tempat = "Gedung Gereja Ebenhaezer Oeba",
                KategoriIbadahId = 2,
                PendetaId = 2,
            }
        );

        return modelBuilder;
    }
}
