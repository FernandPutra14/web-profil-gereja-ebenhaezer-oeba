using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Models.IbadahModels;
using PKMGerejaEbenhaezer.Web.Services.BeebleApi;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class IbadahController : Controller
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IBeebeleApiService _beebeleApiService;

        public IbadahController(IAppDbContext appDbContext, IBeebeleApiService beebeleApiService)
        {
            _appDbContext = appDbContext;
            _beebeleApiService = beebeleApiService;
        }

        public async Task<IActionResult> Index(int? bulan, int? tahun, string? searchString,
            int pageIndex = 1)
        {
            var daftarIbadah = await _appDbContext.IbadahTable
                .Include(i => i.Pendeta).ThenInclude(p => p.Foto)
                .Include(i => i.KategoriIbadah)
                .Where(i => i.Pendeta != null && i.KategoriIbadah != null)
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
            var ibadah = await _appDbContext.IbadahTable
                .Include(i => i.KategoriIbadah)
                .Include(i => i.Pendeta).ThenInclude(p => p.Foto)
                .Where(i => i.Id == id)
                .Where(i => i.Pendeta != null && i.KategoriIbadah != null)
                .AsNoTracking().FirstOrDefaultAsync();

            if (ibadah is null) return NotFound();

            var isiNasPembimbing = await _beebeleApiService.PassageContent(ibadah.NasPembimbing);

            var model = new DetailVM
            {
                Ibadah = ibadah,
                NasPembimbing = isiNasPembimbing,
            };

            if(ibadah.Renungan is not null)
            {
                var isiRenungan = await _beebeleApiService.PassageContent(ibadah.Renungan);
                model.Renungan = isiRenungan;
            }

            return View(model);
        }
    }
}
