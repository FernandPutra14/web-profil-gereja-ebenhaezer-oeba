using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Home;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;

		public HomeController(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<IActionResult> Index()
        {
            var daftarRayon = await _appDbContext.RayonTable.AsNoTracking().ToListAsync();

            return View(new IndexVM
            {
                TotalAnak = daftarRayon.Sum(r => r.JumlahAnak),
                TotalRemaja = daftarRayon.Sum(r => r.JumlahRemaja),
                TotalPemuda = daftarRayon.Sum(r => r.JumlahPemuda),
                TotalDewasa = daftarRayon.Sum(r => r.JumlahDewasa),
                TotalLansia = daftarRayon.Sum(r => r.JumlahLansia),
            });
        }
    }
}
