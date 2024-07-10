using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class WartaJemaatController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<WartaJemaatController> _logger;

        public WartaJemaatController(AppDbContext appDbContext, 
            ILogger<WartaJemaatController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? bulan = null, 
            int? tahun = null, int pageIndex = 1)
        {
            if (bulan is not null && (bulan < 1 || bulan > 12))
                bulan = null;

            var daftarWarta = await _appDbContext.WartaJemaatTable
                .OrderByDescending(w => w.TanggalWarta)
                .AsNoTracking().ToListAsync();

            if (tahun is not null)
                daftarWarta = daftarWarta.Where(w => w.TanggalWarta.Year == tahun).ToList();

            if(bulan is not null)
                daftarWarta = daftarWarta.Where(w => w.TanggalWarta.Month == bulan).ToList();

            var pageSize = 5;

            var items = new PaginatedList<WartaJemaat>(daftarWarta, daftarWarta.Count, pageIndex, pageSize);

            return View(new IndexVM<WartaJemaat>
            {
                Items = items,
                Tahun = tahun,
                Bulan = bulan
            });
        }
    }
}
