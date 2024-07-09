using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengumuman;
using PKMGerejaEbenhaezer.Web.Services.PDF;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class PengumumanController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<PengumumanController> _logger;
        private readonly IPDFUploadService _pDFUploadService;
        private readonly IToastrNotificationService _notificationService;

        public PengumumanController(
            AppDbContext appDbContext,
            ILogger<PengumumanController> logger,
            IPDFUploadService pDFUploadService,
            IToastrNotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _logger = logger;
            _pDFUploadService = pDFUploadService;
            _notificationService = notificationService;
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
            //Validasi
            if (!ModelState.IsValid)
            {
                return View(tambahVM);
            }

            var foto = await _appDbContext.FotoTable.Where(f => f.Id == tambahVM.IdFoto)
                .FirstOrDefaultAsync();

            if (foto is null)
            {
                ModelState.AddModelError(nameof(tambahVM.IdFoto), "Foto tidak ditemukan");
                return View(tambahVM);
            }

            if (tambahVM.HaveDocument && tambahVM.PDFFormFile is null)
            {
                ModelState.AddModelError(nameof(tambahVM.PDFFormFile), "Dokumen harus ada jika Ada Dokumen di centang!");
                return View(tambahVM);
            }

            //Buat Pengumuman
            var newPengumuman = new Pengumuman
            {
                Id = 0,
                Judul = tambahVM.Judul,
                Isi = tambahVM.Isi,
                Foto = foto,
                HaveDocument = tambahVM.HaveDocument
            };

            //Simpan File PDF
            if (tambahVM.HaveDocument)
            {
                var pdfPath = await _pDFUploadService.UploadAsync<TambahVM>(ModelState, tambahVM.PDFFormFile!);
                if (!ModelState.IsValid || pdfPath is null) return View(tambahVM);
                newPengumuman.PathPDF = pdfPath;
            }

            //Simpan pengumuman ke database
            _appDbContext.PengumumanTable.Add(newPengumuman);

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

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Pengumuman baru sukses ditambahkan"
            });
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

            if (pengumuman.HaveDocument && !System.IO.File.Exists(pengumuman.PathPDF))
                _notificationService.AddNotification(new ToastrNotification
                {
                    Type = ToastrNotificationType.Warning,
                    Title = "File Dokumen Pengumuman Tidak Ada",
                    Message = "Hilangkan centang Ada Dokumen atau upload file PDF baru"
                });

            if (pengumuman.Foto is null)
                _notificationService.AddNotification(new ToastrNotification
                {
                    Type = ToastrNotificationType.Warning,
                    Title = "Pengumuman Tidak Ada Foto",
                    Message = "Upload foto baru atau pilih dari foto yang sudah ada"
                });

            return View(new EditVM
            {
                Id = id,
                Judul = pengumuman.Judul,
                Isi = pengumuman.Isi,
                IdFoto = pengumuman.Foto?.Id,
                HaveDocument = pengumuman.HaveDocument,
                OldDocumentExist = pengumuman.HaveDocument,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditVM editVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(editVM);
            var pengumuman = _appDbContext.PengumumanTable.Where(p => p.Id == editVM.Id).FirstOrDefault();

            if (pengumuman is null)
            {
                ModelState.AddModelError(string.Empty, "Pengumuman dengan yang akan diubah tidak ditemukan");
                return View(editVM);
            }

            if (editVM.IdFoto is not null)
            {
                var foto = await _appDbContext.FotoTable.Where(f => f.Id == editVM.IdFoto)
                    .AsNoTracking().FirstOrDefaultAsync();

                if (foto is null)
                {
                    ModelState.AddModelError(nameof(editVM.IdFoto), "Foto tidak ditemukan");
                    return View(editVM);
                }
            }

            if (editVM.HaveDocument && !pengumuman.HaveDocument && editVM.PDFFormFile is null)
            {
                ModelState.AddModelError(nameof(editVM.PDFFormFile), "Dokumen harus diisi jika Ada Dokumen di centang!");
                return View(editVM);
            }

            //Update Pengumuman
            pengumuman.Judul = editVM.Judul;
            pengumuman.Isi = editVM.Isi;

            if (editVM.IdFoto is not null)
                pengumuman.Foto = await _appDbContext.FotoTable.Where(f => f.Id == editVM.IdFoto)
                    .AsNoTracking().FirstOrDefaultAsync();

            if (editVM.HaveDocument)
            {
                if(editVM.PDFFormFile is not null)
                {
                    var pdfPath = await _pDFUploadService.UploadAsync<EditVM>(ModelState, editVM.PDFFormFile);
                    if (!ModelState.IsValid || pdfPath is null)
                        return View(editVM);

                    if (pengumuman.HaveDocument && System.IO.File.Exists(pengumuman.PathPDF))
                        System.IO.File.Delete(pengumuman.PathPDF);

                    pengumuman.PathPDF = pdfPath;
                }
            }
            else
            {
                if (pengumuman.HaveDocument && System.IO.File.Exists(pengumuman.PathPDF))
                    System.IO.File.Delete(pengumuman.PathPDF);

                pengumuman.PathPDF = null;
            }

            pengumuman.HaveDocument = editVM.HaveDocument;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Edit Pengumuman. Exception : {0}", ex.ToString());
                ModelState.AddModelError(string.Empty,
                    "Error terjadi saat mencoba menyimpan perubahan ke database. Silahkan hubungi administrator");
                return View(editVM);
            }

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Pengumuman berhasil diubah"
            });
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
                _logger.LogError("Hapus Pengumuman Gagal! Exception : {0}", ex.Message);
                _notificationService.AddNotification(new ToastrNotification
                {
                    Type = ToastrNotificationType.Error,
                    Title = "Hapus Pengumuman Gagal",
                    Message = "Error terjadi ssat mencoba menghapus data dari database. Silahkan hubungi administrator"
                });
                return Redirect(returnUrl!);
            }

            //Hapus PDF
            if (pengumuman.HaveDocument)
            {
                try
                {
                    if (System.IO.File.Exists(pengumuman.PathPDF))
                    {
                        System.IO.File.Delete(pengumuman.PathPDF);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Hapus PDF Pengumuman Gagal! Exception : {0}", ex.Message);
                    _notificationService.AddNotification(new ToastrNotification
                    {
                        Type = ToastrNotificationType.Error,
                        Title = "Hapus File PDF Pengumuman Gagal",
                        Message = "Pengumuman berhasil dihapus tapi file PDF-nya tidak! Laporkan error ini ke administrator!"
                    });
                    return Redirect(returnUrl!);
                }
            }

            _notificationService.AddNotification(new ToastrNotification
            {
                Type = ToastrNotificationType.Success,
                Title = "Pengumuman berhasil dihapus"
            });
            return Redirect(returnUrl!);
        }
    }
}