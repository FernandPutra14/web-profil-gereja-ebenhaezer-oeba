using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.Domain.Entity;

namespace PKMGerejaEbenhaezer.DataAccess.Data
{
    public interface IAppDbContext
    {
        DbSet<AppUser> AppUserTable { get; set; }
        DbSet<Foto> FotoTable { get; set; }
        DbSet<Ibadah> IbadahTable { get; set; }
        DbSet<KategoriIbadah> KategoriIbadahTable { get; set; }
        DbSet<Pendeta> PendetaTable { get; set; }
        DbSet<Pengumuman> PengumumanTable { get; set; }
        DbSet<Rayon> RayonTable { get; set; }
        DbSet<WartaJemaat> WartaJemaatTable { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}