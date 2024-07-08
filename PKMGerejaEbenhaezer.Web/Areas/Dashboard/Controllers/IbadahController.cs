using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Ibadah;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class IbadahController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<IbadahController> _logger;

        public IbadahController(AppDbContext appDbContext, ILogger<IbadahController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var daftarIbadah = await _appDbContext.IbadahTable
                .Include(i => i.KategoriIbadah)
                .Include(i => i.Pendeta)
                .AsNoTracking().ToListAsync();

            return View(daftarIbadah);
        }

        //Tambah Ibadah
        public IActionResult Tambah()
        {
            return View(new TambahVM());
        }

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(tambahVM);

            //Simpan ke database
            var kategori = await _appDbContext.KategoriIbadahTable
                .Where(k => k.Id == tambahVM.IdKategoriIbadah).FirstOrDefaultAsync();

            var pendeta = await _appDbContext.PendetaTable
                .Where(p => p.Id == tambahVM.IdPendeta).FirstOrDefaultAsync();

            var ibadah = new Ibadah
            {
                Id = 0,
                Judul = tambahVM.Judul,
                Deskripsi = tambahVM.Deskripsi,
                NasPembimbing = tambahVM.NasPembimbing,
                TanggalIbadah = tambahVM.TanggalIbadah,
                Tempat = tambahVM.Tempat,
                KategoriIbadah = kategori,
                Pendeta = pendeta,
            };

            _appDbContext.IbadahTable.Add(ibadah);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Simpan gagal. Laporkan error ke administrator");
                _logger.LogError("Tambah. Simpan Gagal. Exception {0}", ex.ToString());
                return View(tambahVM);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
