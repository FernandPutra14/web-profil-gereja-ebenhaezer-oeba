using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUserTable { get; set; }
        public DbSet<Pengumuman> PengumumanTable {  get; set; }
        public DbSet<Rayon> RayonTable { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    entry.State = EntityState.Detached;
                }
                throw;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasKey(e => e.Id);
            modelBuilder.Entity<AppUser>().HasIndex(e => e.UserName);

            modelBuilder.Entity<AppUser>()
                .HasData(
                    new AppUser
                    {
                        Id = 1,
                        UserName = "admin",
                        Password = new PasswordHasher<AppUser>().HashPassword(null, "admin"),
                    }
                );

            modelBuilder.Entity<Pengumuman>().HasKey(e => e.Id);
            modelBuilder.Entity<Pengumuman>().Property(e => e.Tanggal).HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Pengumuman>().HasData(
                new Pengumuman
                {
                    Id = 1,
                    Judul = "Ibadah Natal Bersama",
                    Keterangan = "Kami mengundang seluruh jemaat untuk hadir dalam Ibadah Natal yang akan diadakan pada hari Minggu, 25 Desember 2024, pukul 10.00 WIB di Gereja. Mari kita rayakan kelahiran Yesus Kristus dengan sukacita dan kebersamaan. Setelah ibadah, akan diadakan acara ramah tamah di aula gereja.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\pengumuman\\natal.jpg",
                },
                new Pengumuman
                {
                    Id = 2,
                    Judul = "Rapat Anggota Jemaat Tahunan",
                    Keterangan = "Rapat Anggota Jemaat Tahunan akan dilaksanakan pada hari Sabtu, 27 Juni 2024, pukul 14.00 WIB di aula gereja. Kami mengajak seluruh jemaat untuk hadir dan berpartisipasi dalam membahas laporan tahunan dan rencana kegiatan gereja untuk tahun 2024.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\pengumuman\\rapat.jpg",
                },
                new Pengumuman
                {
                    Id = 3,
                    Judul = "Retret Pemuda Gereja",
                    Keterangan = "Kami mengundang para pemuda gereja untuk mengikuti Retret Pemuda yang akan diadakan pada tanggal 15-17 Juli 2024 di Wisma Retreat Agape. Tema retret kali ini adalah \"Membangun Iman yang Kuat di Era Digital\". Pendaftaran dapat dilakukan melalui sekretariat gereja hingga 1 Juli 2024.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\pengumuman\\trip.jpg",
                },
                new Pengumuman
                {
                    Id = 4,
                    Judul = "Penggalangan Dana untuk Renovasi Gereja",
                    Keterangan = "Gereja kita akan mengadakan penggalangan dana untuk renovasi bangunan gereja yang direncanakan mulai bulan Juni 2024. Kami mengajak seluruh jemaat untuk berpartisipasi dalam acara ini pada hari Minggu, 5 Juli 2024, pukul 09.00 WIB setelah kebaktian. Donasi dapat diberikan langsung atau melalui rekening gereja.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\pengumuman\\donasi.jpg",
                },
                new Pengumuman
                {
                    Id = 5,
                    Judul = "Pelayanan Sosial Natal",
                    Keterangan = "Dalam rangka menyambut Natal, gereja akan mengadakan kegiatan pelayanan sosial ke Panti Asuhan Kasih Ibu pada hari Sabtu, 23 Desember 2024. Kami mengundang jemaat untuk ikut serta dalam kegiatan ini dengan menyumbangkan pakaian layak pakai, mainan, dan sembako. Barang-barang sumbangan dapat dikumpulkan di kantor gereja hingga 21 Desember 2024.",
                    Tanggal = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PathFoto = "\\img\\pengumuman\\pelayanan.jpg",
                }
            );

            modelBuilder.Entity<Rayon>().HasKey(r => r.Id);

            modelBuilder.Entity<Rayon>().HasData(
                new Rayon
                {
                    Id = 1,
                    Nama = "Rayon I",
                    KetuaRayon = "Ketua Rayon I",
                    FotoKetua = File.ReadAllBytes(@"wwwroot/img/generaluser.png"),
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                },
                new Rayon
                {
                    Id = 2,
                    Nama = "Rayon II",
                    KetuaRayon = "Ketua Rayon II",
                    FotoKetua = File.ReadAllBytes(@"wwwroot/img/generaluser.png"),
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                },
                new Rayon
                {
                    Id = 3,
                    Nama = "Rayon III",
                    KetuaRayon = "Ketua Rayon III",
                    FotoKetua = File.ReadAllBytes(@"wwwroot/img/generaluser.png"),
                    JumlahLakiLaki = 25,
                    JumlahPerempuan = 35,
                    JumlahAnak = 10,
                    JumlahRemaja = 10,
                    JumlahPemuda = 10,
                    JumlahDewasa = 15,
                    JumlahLansia = 15,
                }
            );
        }
    }
}
