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

        [ResponseCache(Duration = 15, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index()
        {
            var daftarPengumuman = await _appDbContext.PengumumanTable
                .OrderByDescending(p => p.TanggalDiBuat)
                .Include(p => p.Foto)
                .Include(p => p.Pembuat)
                .Take(3)
                .AsNoTracking().ToListAsync();

            var daftarWarta = await _appDbContext.WartaJemaatTable
                .OrderByDescending(p => p.TanggalWarta)
                .Include(w => w.Pembuat)
                .Take(3)
                .AsNoTracking().ToListAsync();

            var daftarPendeta = await _appDbContext.PendetaTable
                .Include(p => p.Foto)
                .AsNoTracking().ToListAsync();

            var daftarIbadah = await _appDbContext.IbadahTable
                .Include(i => i.Pendeta).ThenInclude(p => p.Foto)
                .Include(i => i.KategoriIbadah)
                .Where(i => i.Pendeta != null && i.KategoriIbadah != null)
                .OrderByDescending(i => i.TanggalIbadah)
                .Take(3).AsNoTracking().ToListAsync();

            var daftarRayon = await _appDbContext.RayonTable.AsNoTracking().ToListAsync();

            daftarRayon ??= new List<Rayon>();

            return View(new IndexVM
            {
                DaftarPengumuman = daftarPengumuman,
                DaftarWartaJemaat = daftarWarta,
                DaftarPendeta = daftarPendeta,
                DaftarIbadah = daftarIbadah,
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

        public IActionResult StatusCode404()
        {
            return View(); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
