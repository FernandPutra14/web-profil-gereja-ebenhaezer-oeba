using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengumuman;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Utlities;
using System.Drawing;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class PengumumanController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<PengumumanController> _logger;

        public PengumumanController(
            AppDbContext appDbContext,
            ILogger<PengumumanController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var daftarPengumuman = await _appDbContext.PengumumanTable
                .Include(p => p.Foto)
                .Include(p => p.Pembuat)
                .AsNoTracking().ToListAsync();

            daftarPengumuman ??= new List<Pengumuman>();

            return View(daftarPengumuman);
        }

        public IActionResult Tambah()
        {
            return View(new TambahVM());
        }

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            if (!ModelState.IsValid)
            {
                return View(tambahVM);
            }

            var foto = await _appDbContext.FotoTable.Where(f => f.Id == tambahVM.IdFoto)
                .FirstOrDefaultAsync();

            if(foto == null)
            {
                ModelState.AddModelError(nameof(tambahVM.IdFoto), "Foto tidak ditemukan");
                return View(tambahVM);
            }

            //Simpan pwngumuman ke database
            var newPengumuman = new Pengumuman
            {
                Id = 0,
                Judul = tambahVM.Judul,
                Isi = tambahVM.Isi,
                Foto = foto,
            };

            var changeTracker = _appDbContext.PengumumanTable.Add(newPengumuman);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error menyimpan data ke database!");
                _logger.LogError("Tambah Pengumuman : {0}", ex.Message);
                return View(tambahVM);
            }

            _logger.LogInformation("Pengumuman dengan Id {0} ditambahkan", changeTracker.Entity.Id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pengumuman = await _appDbContext.PengumumanTable
                .Where(p => p.Id == id)
                .Include(p => p.Foto)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (pengumuman == null) return NotFound();           

            return View(new EditVM
            {
                Id = id,
                Judul = pengumuman.Judul,
                Isi = pengumuman.Isi,
                IdFoto = pengumuman.Foto?.Id,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(editVM);
            var pengumuman = _appDbContext.PengumumanTable.Where(p => p.Id == editVM.Id).FirstOrDefault();

            if (pengumuman == null)
            {
                ModelState.AddModelError(string.Empty, $"Pengumuman dengan Id {editVM.Id} tidak ditemukan");
                return View(editVM);
            }

            if (editVM.IdFoto != null)
            {
                var foto = await _appDbContext.FotoTable.Where(f => f.Id == editVM.IdFoto)
                    .AsNoTracking().FirstOrDefaultAsync();

                if(foto == null)
                {
                    ModelState.AddModelError(nameof(editVM.IdFoto), "Foto tidak ditemukan");
                    return View(editVM);
                }
            }

            //Update Pengumuman
            pengumuman.Judul = editVM.Judul;
            pengumuman.Isi = editVM.Isi;

            if(editVM.IdFoto != null)
            {
                pengumuman.Foto = await _appDbContext.FotoTable.Where(f => f.Id == editVM.IdFoto)
                    .AsNoTracking().FirstOrDefaultAsync();
            }

            await _appDbContext.SaveChangesAsync();

            _logger.LogInformation("Pengumuman dengan id {0} berhasil diupdate", editVM.Id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Hapus(int id, string? returnUrl)
        {
            returnUrl ??= Url.Action("Index", "Pengumuman", new { Area = "Dashboard" });
            ViewData["returnUrl"] = returnUrl;

            //Validasi
            var pengumuman = await _appDbContext.PengumumanTable.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (pengumuman == null) return BadRequest(pengumuman);

            //Hapus pengumuman
            _appDbContext.PengumumanTable.Remove(pengumuman);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Hapus Pengumuman : {0}", ex.Message);
                return Redirect(returnUrl!);
            }

            return Redirect(returnUrl!);
        }
    }
}