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
        private readonly string[] _permittedFileExtension = new string[] {".jpg", ".jpeg"};
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
            if(!ModelState.IsValid)
            {
                return View(tambahVM);
            }

            //Simpan File Foto
            var formFileContent = await FileHelpers.ProcessFormFile<TambahVM>(
                tambahVM.Foto,
                ModelState,
                _permittedFileExtension,
                _sizeLimit);

            if(!ModelState.IsValid)
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
                _logger.LogError(ex.Message);
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
                _logger.LogError(ex.Message);
                return View(tambahVM);
            }

            _logger.LogInformation("Pengumuman dengan Id {0} ditambahkan", changeTracker.Entity.Id);

            return RedirectToAction("Index");
        }
    }
}
