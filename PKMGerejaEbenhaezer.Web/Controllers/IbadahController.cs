using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class IbadahController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public IbadahController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(int? bulan, int? tahun, string? searchString,
            int pageIndex = 1)
        {
            var daftarIbadah = await _appDbContext.IbadahTable
                .Where(i => i.Pendeta != null && i.KategoriIbadah != null)
                .Include(i => i.Pendeta).ThenInclude(p => p!.Foto)
                .Include(i => i.KategoriIbadah)
                .OrderByDescending(i => i.TanggalIbadah)
                .AsNoTracking().ToListAsync();

            if (bulan is not null && (bulan < 1 || bulan > 12))
                bulan = null;

            if (bulan is not null)
                daftarIbadah = daftarIbadah.Where(i => i.TanggalIbadah.Month == bulan).ToList();

            if (tahun is not null && tahun <= 0)
                tahun = null;

            if (tahun is not null)
                daftarIbadah = daftarIbadah.Where(i => i.TanggalIbadah.Year == tahun).ToList();

            if (searchString is not null)
                daftarIbadah = daftarIbadah
                    .Where(i => i.Judul.ToLower().Contains(searchString.ToLower())
                        || i.KategoriIbadah!.Nama.ToLower().Contains(searchString.ToLower()))
                    .ToList();

            var pageSize = 6;

            return View(new IndexVM<Ibadah>
            {
                Items = PaginatedList<Ibadah>.Create(daftarIbadah, pageIndex, pageSize),
                Bulan = bulan,
                Tahun = tahun,
            });
        }

        public async Task<IActionResult> Detail(int id)
        {
            return View();
        }
    }
}
