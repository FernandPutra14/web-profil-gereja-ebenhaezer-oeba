using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Models.IbadahModels;
using PKMGerejaEbenhaezer.Web.Services.BeebleApi;
using PKMGerejaEbenhaezer.Web.Utilities;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class IbadahController : Controller
    {
        private readonly IAppDbContext _appDbContext;

        public IbadahController(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(int? bulan = null, int? tahun = null, string? searchString = null,
            int pageIndex = 1)
        {
            if (bulan is not null && (bulan < 1 || bulan > 12))
                bulan = null;

            if (tahun is not null && tahun <= 0)
                tahun = null;

            if (pageIndex <= 0) pageIndex = 1;

            var daftarIbadah = await _appDbContext.IbadahTable
                .Include(i => i.Pendeta).ThenInclude(p => p.Foto)
                .Include(i => i.KategoriIbadah)
                .Where(i => i.Pendeta != null && i.KategoriIbadah != null)
                .OrderByDescending(i => i.TanggalIbadah)
                .AsNoTracking().ToListAsync();

            if (bulan is not null)
                daftarIbadah = daftarIbadah.Where(i => i.TanggalIbadah.Month == bulan).ToList();

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
                SearchString = searchString
            });
        }

        public async Task<IActionResult> Detail(int id)
        {
            var ibadah = await _appDbContext.IbadahTable
                .Include(i => i.KategoriIbadah)
                .Include(i => i.Pendeta).ThenInclude(p => p.Foto)
                .Where(i => i.Id == id)
                .Where(i => i.Pendeta != null && i.KategoriIbadah != null)
                .AsNoTracking().FirstOrDefaultAsync();

            if (ibadah is null) return NotFound();

            return View(ibadah);
        }
    }
}
