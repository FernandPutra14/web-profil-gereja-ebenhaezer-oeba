using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Models;
using PKMGerejaEbenhaezer.Web.Utilities;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class PengumumanController : Controller
    {
        private readonly IAppDbContext _appDbContext;

        public PengumumanController(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(int? bulan, int? tahun, int? pageIndex, string? searchString)
        {
            if (bulan is not null && (bulan < 1 || bulan > 12)) 
            {
                bulan = null;
            }

            var daftarPengumuman = await _appDbContext.PengumumanTable
                .Include(p => p.Foto)
                .Include(p => p.Pembuat)
                .AsNoTracking().ToListAsync();

            if (bulan is not null)
                daftarPengumuman = daftarPengumuman.Where(p => p.TanggalDiBuat.Month == bulan).ToList();

            if (tahun is not null)
                daftarPengumuman = daftarPengumuman.Where(p => p.TanggalDiBuat.Year == tahun).ToList();

            if (searchString is not null)
                daftarPengumuman = daftarPengumuman
                    .Where(p => p.Judul.ToLower().Contains(searchString.ToLower()) || p.Isi.ToLower().Contains(searchString.ToLower()))
                    .ToList();

            daftarPengumuman = daftarPengumuman.OrderByDescending(p => p.TanggalDiBuat).ToList();

            int pageSize = 6;

            var model = new IndexVM<Pengumuman>
            {
                Items = PaginatedList<Pengumuman>.Create(daftarPengumuman, pageIndex ?? 1, pageSize),
                Bulan = bulan,
                Tahun = tahun,
                SearchString = searchString,
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var pengumuman = await _appDbContext.PengumumanTable.Where(p => p.Id == id)
                .Include(p => p.Foto)
                .Include(p => p.Pembuat)
                .FirstOrDefaultAsync();

            if(pengumuman == null)
                return NotFound();

            return View(pengumuman);
        }

        public async Task<IActionResult> Dokumen(int id)
        {
            var pengumuman = await _appDbContext.PengumumanTable.Where(p => p.Id == id)
                .AsNoTracking().FirstOrDefaultAsync();

            if (pengumuman is null) return NotFound();

            if (!pengumuman.HaveDocument) return BadRequest();

            if (!System.IO.File.Exists(pengumuman.PathPDF)) return NotFound();

            return PhysicalFile(pengumuman.PathPDF, "application/pdf");
        }
    }
}
