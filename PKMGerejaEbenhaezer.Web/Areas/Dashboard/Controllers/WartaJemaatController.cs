using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.WartaJemaat;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
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

        public async Task<IActionResult> Index()
        {
            var daftarWarta = await _appDbContext.WartaJemaatTable
                .Include(w => w.Pembuat)
                .AsNoTracking()
                .ToListAsync();

            return View(daftarWarta);
        }

        //Tambah Warta Jemaat
        public IActionResult Tambah()
        {
            return View(new TambahVM());
        }

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(tambahVM);

            var warta = await _appDbContext.WartaJemaatTable
                .Where(w => w.TanggalWarta.Date == tambahVM.TanggalWarta.Date)
                .FirstOrDefaultAsync();

            if (warta is not null)
            {
                ModelState.AddModelError(nameof(TambahVM.TanggalWarta), "Tanggal Sudah Digunakan Untuk Warta Lain!");
                return View(tambahVM);
            }

            //Simpan ke database
            var wartaBaru = new WartaJemaat
            {
                Id = 0,
                TanggalWarta = tambahVM.TanggalWarta,
                DocumentLink = new Uri(tambahVM.DocumentLink)
            };

            _appDbContext.WartaJemaatTable.Add(wartaBaru);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Simpan Gagal!. Terjadi error saat menyimpan ke database!");
                _logger.LogError("Tambah. Exception : {0}", ex.ToString());
                return View(tambahVM);
            }

            return RedirectToAction(nameof(Index));
        }

        //Edit Warta Jemaat
        public async Task<IActionResult> Edit(int id)
        {
            var warta = await _appDbContext.WartaJemaatTable.Where(w => w.Id == id)
                .AsNoTracking().FirstOrDefaultAsync();

            if (warta is null) return NotFound();

            return View(new EditVM
            {
                Id = id,
                TanggalWarta = warta.TanggalWarta,
                DocumentLink = warta.DocumentLink.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            var warta = await _appDbContext.WartaJemaatTable
                .Where(w => w.Id == editVM.Id)
                .FirstOrDefaultAsync();

            if(warta is null)
            {
                ModelState.AddModelError(string.Empty, "Warta Jemaat Tidak Ditemukan");
                return View(editVM);
            }

            var duplikasiTanggal = await _appDbContext.WartaJemaatTable
                .AnyAsync(w => w.Id != editVM.Id && w.TanggalWarta.Date == editVM.TanggalWarta.Date);

            if (duplikasiTanggal)
            {
                ModelState.AddModelError(nameof(EditVM.TanggalWarta), "Tanggal Sudah Digunakan Untuk Warta Lain!");
                return View(editVM);
            }

            //Simpan ke database
            _appDbContext.WartaJemaatTable.Update(warta);
            warta.TanggalWarta = editVM.TanggalWarta;
            warta.DocumentLink = new Uri(editVM.DocumentLink);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, "Simpan Gagal!. Terjadi error saat menyimpan ke database!");
                _logger.LogError("Edit. Exception : {0}", ex.ToString());
                return View(editVM);
            }

            return RedirectToAction(nameof(Index));
        }

        //Hapus Warta Jemaat
        [HttpPost]
        public async Task<IActionResult> Hapus(int id, string? returnUrl)
        {
            returnUrl ??= Url.Action(nameof(Index));

            var warta = await _appDbContext.WartaJemaatTable
                .Where(w => w.Id == id).FirstOrDefaultAsync();

            if (warta is null) return NotFound();

            _appDbContext.WartaJemaatTable.Remove(warta);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Hapus. Exception : {0}", ex.ToString());
            }

            return Redirect(returnUrl!);
        }
    }
}
