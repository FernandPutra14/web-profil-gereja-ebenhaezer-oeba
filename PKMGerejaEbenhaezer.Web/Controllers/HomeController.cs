using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Models.Home;
using System.Diagnostics;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _appDbContext;

        public HomeController(ILogger<HomeController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            var daftarPengumuman = await _appDbContext.PengumumanTable
                .Include(p => p.Foto)
                .Include(p => p.Pembuat)
                .AsNoTracking().ToListAsync();

            var daftarRayon = await _appDbContext.RayonTable.AsNoTracking().ToListAsync();

            daftarRayon ??= new List<Rayon>();

            return View(new IndexVM 
            { 
                DaftarPengumuman = daftarPengumuman,
                TotalAnak = daftarRayon.Sum(r => r.JumlahAnak),
                TotalRemaja = daftarRayon.Sum(r => r.JumlahRemaja),
                TotalPemuda = daftarRayon.Sum(r => r.JumlahPemuda),
                TotalDewasa = daftarRayon.Sum(r => r.JumlahDewasa),
                TotalLansia = daftarRayon.Sum(r => r.JumlahLansia),
            });
        }

        public IActionResult Kontak()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
