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
        public DbSet<WartaJemaat> WartaJemaatTable { get; set; }
        public DbSet<Pendeta> PendetaTable { get; set; }
        public DbSet<KategoriIbadah> KategoriIbadahTable { get; set; }
        public DbSet<Ibadah> IbadahTable { get; set; }

        public override int SaveChanges()
        {
            try
            {
                UpdateAppUserLastChanged();
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
                UpdateAppUserLastChanged();
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

            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

            modelBuilder.SeedingData();
        }

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

        private void UpdateAppUserLastChanged()
        {
            var entries = ChangeTracker.Entries<AppUser>()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (var item in entries)
            {
                item.Entity.LastChanged = DateTime.Now;
            }
        }
    }
}
