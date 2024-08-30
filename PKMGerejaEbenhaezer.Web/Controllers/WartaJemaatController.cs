using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Utilities;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    [OutputCache]
    public class WartaJemaatController : Controller
    {
        private readonly IAppDbContext _appDbContext;

        public WartaJemaatController(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(int? bulan = null, int? tahun = null, int pageIndex = 1)
        {
            if (bulan is not null && (bulan < 1 || bulan > 12))
                bulan = null;

            if(tahun is not null && tahun <= 0)
                tahun = null;

            if(pageIndex <= 0) pageIndex = 1;

            var daftarWarta = await _appDbContext.WartaJemaatTable
                .OrderByDescending(w => w.TanggalWarta)
                .AsNoTracking().ToListAsync();

            if (tahun is not null)
                daftarWarta = daftarWarta.Where(w => w.TanggalWarta.Year == tahun).ToList();

            if(bulan is not null)
                daftarWarta = daftarWarta.Where(w => w.TanggalWarta.Month == bulan).ToList();

            var pageSize = 6;

            var items = PaginatedList<WartaJemaat>.Create(daftarWarta, pageIndex, pageSize);

            return View(new IndexVM<WartaJemaat>
            {
                Items = items,
                Tahun = tahun,
                Bulan = bulan
            });
        }
    }
}
