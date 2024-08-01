using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pendeta;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class PendetaController : Controller
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<PendetaController> _logger;
        private readonly IToastrNotificationService _notificationService;

        public PendetaController(IAppDbContext appDbContext,
            ILogger<PendetaController> logger,
            IToastrNotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _notificationService = notificationService;
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
            if (!ModelState.IsValid) return View(tambahVM);

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
                Jabatan = tambahVM.Jabatan?.Trim(),
                Foto = foto
            };

            if(tambahVM.FacebookProfileLink is not null)
                pendeta.FacebookProfileLink = new Uri(tambahVM.FacebookProfileLink);

            if(tambahVM.InstagramProfileLink is not null)
                pendeta.InstagramProfileLink = new Uri(tambahVM.InstagramProfileLink);

            if(tambahVM.YoutubeProfileLink is not null)
                pendeta.YoutubeProfileLink = new Uri(tambahVM.YoutubeProfileLink);

            if (!ValidasiAkunMediaSosial(pendeta.FacebookProfileLink, pendeta.InstagramProfileLink,
                pendeta.YoutubeProfileLink))
                return View(tambahVM);
            
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

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Pendeta Baru Berhasil Ditambahkan"
            });
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
                Jabatan = pendeta.Jabatan,
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
            if (!ModelState.IsValid) return View(editVM);

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

            var pendeta = await _appDbContext.PendetaTable
                .Where(p => p.Id == editVM.Id).FirstOrDefaultAsync();

            if (pendeta is null)
            {
                _notificationService.AddNotification(new ToastrNotification
                {
                    Type = ToastrNotificationType.Error,
                    Title = "Edit Data Pendeta Gagal!",
                    Message = "Pendeta yang ingin diedit tidak ditemukan"
                });

                return RedirectToAction(nameof(Index));
            }

            //Simpan ke database
            pendeta.Nama = editVM.Nama.Trim();
            pendeta.Jabatan = editVM.Jabatan?.Trim();
            if (editVM.IdFoto is not null)
            {
                pendeta.Foto = foto;
            }

            if (editVM.FacebookProfileLink is not null)
                pendeta.FacebookProfileLink = new Uri(editVM.FacebookProfileLink);
            else
                pendeta.FacebookProfileLink = null;

            if (editVM.InstagramProfileLink is not null)
                pendeta.InstagramProfileLink = new Uri(editVM.InstagramProfileLink);
            else
                pendeta.InstagramProfileLink = null;

            if (editVM.YoutubeProfileLink is not null)
                pendeta.YoutubeProfileLink = new Uri(editVM.YoutubeProfileLink);
            else
                pendeta.YoutubeProfileLink = null;

            if (!ValidasiAkunMediaSosial(pendeta.FacebookProfileLink, pendeta.InstagramProfileLink,
                pendeta.YoutubeProfileLink))
                return View(editVM);

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

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Pengubahan data pendeta sukses!"
            });
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
                _notificationService.AddNotification(new ToastrNotification
                {
                    Type = ToastrNotificationType.Success,
                    Title = "Hapus Pendeta berhasil"
                });
            }
            catch(Exception ex)
            {
                _logger.LogError("Hapus. Exception : {0}", ex.ToString());
                _notificationService.AddNotification(new ToastrNotification
                {
                    Type = ToastrNotificationType.Error,
                    Title = "Hapus pendeta gagal",
                    Message = "Error terjadi saat mencoba menghapus data dari database. Silahkan hubungi administrator",
                });
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ValidasiAkunMediaSosial(
            Uri? facebookProfileLink, 
            Uri? instagramProfileLink, 
            Uri? youtubeProfileLink)
        {
            bool isValid = true;

            if(facebookProfileLink is not null)
            {
                if(!facebookProfileLink.Host.EndsWith("facebook.com"))
                {
                    ModelState.AddModelError(nameof(facebookProfileLink), "Bukan URL Facebook valid");
                    isValid = false;
                }
            }

            if (instagramProfileLink is not null) 
            {
                if (!instagramProfileLink.Host.EndsWith("instagram.com"))
                {
                    ModelState.AddModelError(nameof(instagramProfileLink), "Bukan URL Instagram Valid");
                    isValid = false;
                }
            }

            if (youtubeProfileLink is not null)
            {
                if (!youtubeProfileLink.Host.EndsWith("youtube.com"))
                {
                    ModelState.AddModelError(nameof(youtubeProfileLink), "Bukan URL Facebook Valid");
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}