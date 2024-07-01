using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public enum Kategori
    {
        Semua, Bulan
    }

    public class PengumumanController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PengumumanController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(Kategori kategori = Kategori.Semua, int bulan = 1)
        {
            var daftarPengumuman = _appDbContext.PengumumanTable
                .Include(p => p.Foto)
                .Include(p => p.Pembuat)
                .AsNoTracking();

            if(kategori == Kategori.Bulan)
            {
                daftarPengumuman = daftarPengumuman.Where(p => p.TanggalDiBuat.Month == bulan)
                    .OrderByDescending(p => p.TanggalDiBuat);

                var modelPerbulan = await daftarPengumuman.ToListAsync();

                return View(modelPerbulan);
            }

            var model = await daftarPengumuman
                .OrderByDescending(p => p.TanggalDiBuat).ToListAsync();

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
