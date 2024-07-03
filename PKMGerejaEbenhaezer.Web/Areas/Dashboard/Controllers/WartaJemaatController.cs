using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class WartaJemaatController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public WartaJemaatController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var daftarWarta = await _appDbContext.WartaJemaatTable
                .Include(w => w.Pembuat)
                .AsNoTracking()
                .ToListAsync();

            return View(daftarWarta);
        }
    }
}
