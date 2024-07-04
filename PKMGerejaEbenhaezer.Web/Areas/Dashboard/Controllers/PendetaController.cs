using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pendeta;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class PendetaController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<PendetaController> _logger;

        public PendetaController(AppDbContext appDbContext, 
            ILogger<PendetaController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var daftarPendeta = await _appDbContext.PendetaTable
                .Include(p => p.Foto)
                .AsNoTracking().ToListAsync();

            return View(daftarPendeta);
        }

        //Tambah
        public IActionResult Tambah()
        {
            return View(new TambahVM());
        }

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            //Validasi
            var duplikasiNama = await _appDbContext.PendetaTable
                .AnyAsync(p => p.Nama.ToLower() == tambahVM.Nama.Trim().ToLower());

            if (duplikasiNama)
            {
                ModelState.AddModelError(nameof(TambahVM.Nama), "Nama sudah digunakan!");
                return View(tambahVM);
            }

            var foto = await _appDbContext.FotoTable
                .Where(f => f.Id == tambahVM.IdFoto).FirstOrDefaultAsync();

            if(foto is null)
            {
                ModelState.AddModelError(nameof(TambahVM.IdFoto), "Foto tidak ditemukan!");
                return View(tambahVM);
            }

            //Simpan ke database
            var pendeta = new Pendeta
            {
                Id = 0,
                Nama = tambahVM.Nama.Trim(),
                Foto = foto
            };

            if(tambahVM.FacebookProfileLink is not null)
                pendeta.FacebookProfileLink = new Uri(tambahVM.FacebookProfileLink);

            if(tambahVM.InstagramProfileLink is not null)
                pendeta.InstagramProfileLink = new Uri(tambahVM.InstagramProfileLink);

            if(tambahVM.YoutubeProfileLink is not null)
                pendeta.YoutubeProfileLink = new Uri(tambahVM.YoutubeProfileLink);
            
            _appDbContext.PendetaTable.Add(pendeta);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, 
                    "Simpan Gagal!. Telah terjadi error saat menyimpan ke database!");
                _logger.LogError("Tambah. Exception : {0}", ex.ToString());
                return View(tambahVM);
            }

            return RedirectToAction(nameof(Index));
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var pendeta = await _appDbContext.PendetaTable
                .Where(p => p.Id == id)
                .Include(p => p.Foto).FirstOrDefaultAsync();

            if (pendeta is null) return NotFound();

            return View(new EditVM
            {
                Id = pendeta.Id,
                Nama = pendeta.Nama,
                IdFoto = pendeta.Foto?.Id,
                FacebookProfileLink = pendeta.FacebookProfileLink?.ToString(),
                InstagramProfileLink = pendeta.InstagramProfileLink?.ToString(),
                YoutubeProfileLink = pendeta.YoutubeProfileLink?.ToString(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            var pendeta = await _appDbContext.PendetaTable
                .Where(p => p.Id == editVM.Id).FirstOrDefaultAsync();

            if (pendeta is null)
                return RedirectToAction(nameof(Index));

            var duplikasiNama = await _appDbContext.PendetaTable
                .AnyAsync(p => p.Id != editVM.Id && p.Nama.ToLower() == editVM.Nama.Trim().ToLower());

            if (duplikasiNama)
            {
                ModelState.AddModelError(nameof(EditVM.Nama), "Nama sudah digunakan!");
                return View(editVM);
            }

            Foto? foto = null;
            if(editVM.IdFoto is not null)
            {
                foto = await _appDbContext.FotoTable
                    .Where(f => f.Id == editVM.IdFoto).FirstOrDefaultAsync();

                if (foto is null)
                {
                    ModelState.AddModelError(nameof(EditVM.IdFoto), "Foto tidak ditemukan!");
                    return View(editVM);
                }
            }

            //Simpan ke database
            pendeta.Nama = editVM.Nama.Trim();

            if(editVM.IdFoto is not null)
            {
                pendeta.Foto = foto;
            }

            if (editVM.FacebookProfileLink is not null)
                pendeta.FacebookProfileLink = new Uri(editVM.FacebookProfileLink);

            if (editVM.InstagramProfileLink is not null)
                pendeta.InstagramProfileLink = new Uri(editVM.InstagramProfileLink);

            if (editVM.YoutubeProfileLink is not null)
                pendeta.YoutubeProfileLink = new Uri(editVM.YoutubeProfileLink);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    "Simpan Gagal!. Telah terjadi error saat menyimpan ke database!");
                _logger.LogError("Edit. Exception : {0}", ex.ToString());
                return View(editVM);
            }

            return RedirectToAction(nameof(Index));
        }

        //Hapus
        [HttpPost]
        public async Task<IActionResult> Hapus(int id)
        {
            var pendeta = await _appDbContext.PendetaTable
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (pendeta is null) return NotFound();

            _appDbContext.PendetaTable.Remove(pendeta);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("Hapus. Exception : {0}", ex.ToString());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}