using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Pengumuman;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class PengumumanController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly string[] _permittedFileExtension = new string[] { ".jpg", ".jpeg" };
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<PengumumanController> _logger;
        private readonly long _sizeLimit = 8000000L; //8 MB

        public PengumumanController(
            AppDbContext appDbContext,
            IWebHostEnvironment webHostEnvironment,
            ILogger<PengumumanController> logger)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var daftarPengumuman = await _appDbContext.PengumumanTable
                .AsNoTracking().ToListAsync();

            daftarPengumuman ??= new List<Pengumuman>();

            return View(daftarPengumuman);
        }

        public IActionResult Tambah()
        {
            return View(new TambahVM
            {
                Tanggal = DateTime.Now,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            if (!ModelState.IsValid)
            {
                return View(tambahVM);
            }

            //Simpan File Foto
            var formFileContent = await FileHelpers.ProcessFormFile<TambahVM>(
                tambahVM.Foto,
                ModelState,
                _permittedFileExtension,
                _sizeLimit);

            if (!ModelState.IsValid)
            {
                return View(tambahVM);
            }

            var namaFile = $"Pengumuman-{Path.GetRandomFileName()}{Path.GetExtension(tambahVM.Foto.FileName)}";
            var pathFoto = Path.Combine("/img/", namaFile);

            try
            {
                using (var fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + pathFoto))
                {
                    await fileStream.WriteAsync(formFileContent);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error menyimpan file foto!");
                _logger.LogError("Tambah Pengumuman : {0}", ex.Message);
                return View(tambahVM);
            }

            _logger.LogInformation("File {0} berhasil disimpan", pathFoto);

            //Simpan pwngumuman ke database
            var newPengumuman = new Pengumuman
            {
                Id = 0,
                Judul = tambahVM.Judul,
                Keterangan = tambahVM.Keterangan,
                Tanggal = tambahVM.Tanggal,
                PathFoto = pathFoto,
            };

            var changeTracker = _appDbContext.PengumumanTable.Add(newPengumuman);
            var result = 0;

            try
            {
                result = await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.IO.File.Delete(_webHostEnvironment.WebRootPath + pathFoto);

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
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (pengumuman == null)
            {
                _logger.LogError("Edit pengumuman gagal. Tidak ada pengumuman dengan id {0}", id);
                return RedirectToAction("Index", "Pengumuman");
            }

            return View(new EditVM
            {
                Id = id,
                Judul = pengumuman.Judul,
                Keterangan = pengumuman.Keterangan,
                Tanggal = pengumuman.Tanggal,
                PathFoto = pengumuman.PathFoto
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

            _appDbContext.PengumumanTable.Update(pengumuman);

            //Simpan foto baru dan hapus foto lama
            if (editVM.FotoBaru != null)
            {
                var fileFormContent = await FileHelpers.ProcessFormFile<EditVM>(
                    editVM.FotoBaru,
                    ModelState,
                    _permittedFileExtension,
                    _sizeLimit);

                if (!ModelState.IsValid) return View(editVM);

                var namaFile = $"Pengumuman-{Path.GetRandomFileName()}{Path.GetExtension(editVM.FotoBaru.FileName)}";
                var pathFotoBaru = Path.Combine("/img/", namaFile);

                try
                {
                    using (var fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + pathFotoBaru)) 
                    {
                        await fileStream.WriteAsync(fileFormContent);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(editVM.FotoBaru.Name, "Foto baru gagal disimpan!");
                    _logger.LogError("Simpan foto baru gagal! {0}", ex.Message);

                    return View(editVM);
                }

                if(System.IO.File.Exists(_webHostEnvironment + pengumuman.PathFoto))
                {
                    System.IO.File.Delete(_webHostEnvironment + pengumuman.PathFoto);
                }

                pengumuman.PathFoto = pathFotoBaru;
            }

            //Update Pengumuman
            pengumuman.Judul = editVM.Judul;
            pengumuman.Keterangan = editVM.Keterangan;
            pengumuman.Tanggal = editVM.Tanggal;

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
            if (pengumuman == null) return Redirect(returnUrl!);


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

            //Hapus Foto
            try
            {
                System.IO.File.Delete(_webHostEnvironment + pengumuman.PathFoto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Hapus Pengumuman - Hapus Foto : {0}", ex.Message);
                return Redirect(returnUrl!);
            }

            _logger.LogInformation("Pengumunan {0} berhasil dihapus", pengumuman.Id);
            return Redirect(returnUrl!);
        }
    }
}