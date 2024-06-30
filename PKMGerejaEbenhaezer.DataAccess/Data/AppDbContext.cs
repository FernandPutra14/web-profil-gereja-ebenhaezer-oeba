using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Domain.Entity.Contracts;
using System.Security.Claims;

namespace PKMGerejaEbenhaezer.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        private readonly HttpContext _httpContext;
        private readonly IHostEnvironment _webHostEnviroment;

        public AppDbContext(DbContextOptions<AppDbContext> options,
            IHttpContextAccessor httpContextAccessor,
            IHostEnvironment webHostEnviroment) : base(options)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _webHostEnviroment = webHostEnviroment;
        }

        public DbSet<AppUser> AppUserTable { get; set; }
        public DbSet<Pengumuman> PengumumanTable { get; set; }
        public DbSet<Rayon> RayonTable { get; set; }
        public DbSet<Foto> FotoTable { get; set; }

        private void AuditAuditableEntity()
        {
            var addedEntries = ChangeTracker.Entries<IAuditableEntity>()
                    .Where(e => e.State == EntityState.Added);

            var modifiedEntries = ChangeTracker.Entries<IAuditableEntity>()
                .Where(e => e.State == EntityState.Modified);

            var userName = _httpContext.User.Claims.Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .FirstOrDefault();
            var user = AppUserTable.Where(u => u.UserName == userName).FirstOrDefault();

            if (addedEntries != null && addedEntries.Count() > 0)
            {
                foreach (var entry in addedEntries)
                {
                    entry.Entity.TanggalDiBuat = DateTime.Now;
                    entry.Entity.Pembuat = user;
                }
            }

            if (modifiedEntries != null && modifiedEntries.Count() > 0)
            {
                foreach (var entry in modifiedEntries)
                {
                    entry.Entity.TanggalDiUbah = DateTime.Now;
                }
            }
        }

        public override int SaveChanges()
        {
            try
            {
                AuditAuditableEntity();
                return base.SaveChanges();
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                AuditAuditableEntity();

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

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property(nameof(IAuditableEntity.TanggalDiBuat))
                        .HasColumnType("timestamp without time zone");
                    modelBuilder.Entity(entityType.ClrType).Property(nameof(IAuditableEntity.TanggalDiUbah))
                        .HasColumnType("timestamp without time zone");
                    modelBuilder.Entity(entityType.ClrType).HasOne(nameof(IAuditableEntity.Pembuat))
                        .WithMany().OnDelete(DeleteBehavior.SetNull);
                }
            }

            modelBuilder.Entity<AppUser>().HasKey(e => e.Id);
            modelBuilder.Entity<AppUser>().HasIndex(e => e.UserName);

            modelBuilder.Entity<Foto>().HasKey(e => e.Id);

            modelBuilder.Entity<Pengumuman>().HasKey(e => e.Id);
            modelBuilder.Entity<Pengumuman>().HasOne(e => e.Foto).WithMany().OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Rayon>().HasKey(r => r.Id);
            modelBuilder.Entity<Rayon>().HasOne(e => e.FotoKetua).WithMany().OnDelete(DeleteBehavior.SetNull);

            ///Seeding Data
            var user = new AppUser
            {
                Id = 1,
                UserName = "admin",
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "admin"),
            };

            modelBuilder.Entity<AppUser>().HasData(user);

            var daftarFoto = new[]
            {
                new
                {
                    Id = 1,
                    PathFoto = "\\wwwroot\\img\\pengumuman\\natall.jpg",
                    PathFotoKompresi = "\\wwwroot\\img\\pengumuman\\natall.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = user.Id
                },
                new
                {
                    Id = 2,
                    PathFoto = "\\wwwroot\\img\\pengumuman\\rapatt.jpg",
                    PathFotoKompresi = "\\wwwroot\\img\\pengumuman\\rapatt.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = user.Id
                },
                new
                {
                    Id = 3,
                    PathFoto = "\\wwwroot\\img\\pengumuman\\tripp.jpg",
                    PathFotoKompresi = "\\wwwroot\\img\\pengumuman\\tripp.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = user.Id
                },
                new
                {
                    Id = 4,
                    PathFoto = "\\wwwroot\\img\\pengumuman\\donasii.jpg",
                    PathFotoKompresi = "\\wwwroot\\img\\pengumuman\\donasii.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = user.Id
                },
                new
                {
                    Id = 5,
                    PathFoto = "\\wwwroot\\img\\pengumuman\\pelayanann.jpg",
                    PathFotoKompresi = "\\wwwroot\\img\\pengumuman\\pelayanann.jpg",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = user.Id
                },
                new
                {
                    Id = 6,
                    PathFoto = @"/wwwroot/img/generaluser.png",
                    PathFotoKompresi = @"/wwwroot/img/generaluser.png",
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    PembuatId = user.Id
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
                    PembuatId = user.Id
                },
                new
                {
                    Id = 2,
                    Judul = "Rapat Anggota Jemaat Tahunan",
                    Isi = "Rapat Anggota Jemaat Tahunan akan dilaksanakan pada hari Sabtu, 27 Juni 2024, pukul 14.00 WIB di aula gereja. Kami mengajak seluruh jemaat untuk hadir dan berpartisipasi dalam membahas laporan tahunan dan rencana kegiatan gereja untuk tahun 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[1].Id,
                    PembuatId = user.Id
                },
                new
                {
                    Id = 3,
                    Judul = "Retret Pemuda Gereja",
                    Isi = "Kami mengundang para pemuda gereja untuk mengikuti Retret Pemuda yang akan diadakan pada tanggal 15-17 Juli 2024 di Wisma Retreat Agape. Tema retret kali ini adalah \"Membangun Iman yang Kuat di Era Digital\". Pendaftaran dapat dilakukan melalui sekretariat gereja hingga 1 Juli 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[2].Id,
                    PembuatId = user.Id
                },
                new
                {
                    Id = 4,
                    Judul = "Penggalangan Dana untuk Renovasi Gereja",
                    Isi = "Gereja kita akan mengadakan penggalangan dana untuk renovasi bangunan gereja yang direncanakan mulai bulan Juni 2024. Kami mengajak seluruh jemaat untuk berpartisipasi dalam acara ini pada hari Minggu, 5 Juli 2024, pukul 09.00 WIB setelah kebaktian. Donasi dapat diberikan langsung atau melalui rekening gereja.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[3].Id,
                    PembuatId = user.Id
                },
                new
                {
                    Id = 5,
                    Judul = "Pelayanan Sosial Natal",
                    Isi = "Dalam rangka menyambut Natal, gereja akan mengadakan kegiatan pelayanan sosial ke Panti Asuhan Kasih Ibu pada hari Sabtu, 23 Desember 2024. Kami mengundang jemaat untuk ikut serta dalam kegiatan ini dengan menyumbangkan pakaian layak pakai, mainan, dan sembako. Barang-barang sumbangan dapat dikumpulkan di kantor gereja hingga 21 Desember 2024.",
                    HaveDocument = false,
                    TanggalDiBuat = new DateTime(2024, 5, 22, 0, 0, 0, DateTimeKind.Unspecified),
                    FotoId = daftarFoto[4].Id,
                    PembuatId = user.Id
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
        }
    }
}
