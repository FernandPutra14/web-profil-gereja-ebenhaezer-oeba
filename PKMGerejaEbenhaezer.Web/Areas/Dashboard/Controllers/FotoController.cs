using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.FotoModels;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Utlities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class FotoController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly PhotoFileSettingsOptions _photoFileSettingsOptions;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FotoController> _logger;

        public FotoController(AppDbContext appDbContext,
            PhotoFileSettingsOptions photoFileSettingsOptions,
            IWebHostEnvironment webHostEnvironment,
            ILogger<FotoController> logger)
        {
            _appDbContext = appDbContext;
            _photoFileSettingsOptions = photoFileSettingsOptions;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? pageIndex)
        {
            var daftarFoto = await _appDbContext.FotoTable
                .Include(f => f.Pembuat)
                .OrderByDescending(f => f.TanggalDiBuat)
                .AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<Foto>.Create(daftarFoto, pageIndex ?? 1, 16);

            return View(new IndexVM
            {
                Items = paginatedList
            });
        }

        [HttpPost]
        public async Task<IActionResult> IndexPOST(IndexVM indexVM, int pIndex)
        {
            var daftarFoto = await _appDbContext.FotoTable
                .Include(f => f.Pembuat)
                .OrderByDescending(f => f.TanggalDiBuat)
                .AsNoTracking().ToListAsync();

            var paginatedList = PaginatedList<Foto>.Create(daftarFoto, pIndex, 16);

            indexVM.Items = paginatedList;

            //Validasi
            if (!ModelState.IsValid)
            {
                return View("Index", indexVM);
            }

            //Upload File
            var fileFormContent = await FileHelpers.ProcessFormFile<IndexVM>(
                indexVM.FormFile,
                ModelState,
                _photoFileSettingsOptions.PermittedFileExtensions,
                _photoFileSettingsOptions.MinSizeLimit,
                _photoFileSettingsOptions.MaxSizeLimit);

            if (!ModelState.IsValid)
            {
                return View("Index", indexVM);
            }

            var fotoPath = string.Empty;
            var fotoPathKompresi = string.Empty;
            try
            {
                fotoPath = await SaveFile(fileFormContent, Path.GetExtension(indexVM.FormFile.FileName));
                fotoPathKompresi = Path.Combine(Path.GetDirectoryName(fotoPath)!,
                    $"{Path.GetFileNameWithoutExtension(fotoPath)}-kompresi.jpeg");

                using (var fotoKompresi = Image.Load(fileFormContent))
                {
                    var encoder = new JpegEncoder
                    {
                        Quality = _photoFileSettingsOptions.CompressionQuality
                    };
                    fotoKompresi.Save(fotoPathKompresi, encoder);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Upload Foto Gagal. Error: {0}", ex.ToString());
                return View("Index", indexVM);
            }

            var foto = new Foto
            {
                Id = 0,
                PathFoto = fotoPath,
                PathFotoKompresi = fotoPathKompresi,
            };

            var changeTracker = _appDbContext.FotoTable.Add(foto);

            await _appDbContext.SaveChangesAsync();

            daftarFoto = await _appDbContext.FotoTable
                .Include(f => f.Pembuat)
                .OrderByDescending(f => f.TanggalDiBuat)
                .AsNoTracking().ToListAsync();

            paginatedList = PaginatedList<Foto>.Create(daftarFoto, pIndex, 16);

            indexVM.Items = paginatedList;

            return View("Index", indexVM);
        }

        [HttpPost]
        public async Task<IActionResult> TambahJSON(IFormFile formFile)
        {
            //Validasi
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Upload File
            var fileFormContent = await FileHelpers.ProcessFormFile<IFormFile>(
                formFile,
                ModelState,
                _photoFileSettingsOptions.PermittedFileExtensions,
                _photoFileSettingsOptions.MinSizeLimit,
                _photoFileSettingsOptions.MaxSizeLimit);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fotoPath = string.Empty;
            var fotoPathKompresi = string.Empty;
            try
            {
                fotoPath = await SaveFile(fileFormContent, Path.GetExtension(formFile.FileName));
                fotoPathKompresi = Path.Combine(Path.GetDirectoryName(fotoPath)!,
                    $"{Path.GetFileNameWithoutExtension(fotoPath)}-kompresi.jpeg");

                using (var fotoKompresi = Image.Load(fileFormContent))
                {
                    var encoder = new JpegEncoder 
                    { 
                        Quality = _photoFileSettingsOptions.CompressionQuality 
                    };
                    fotoKompresi.Save(fotoPathKompresi, encoder);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Upload Foto Gagal. Error: {0}", ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var foto = new Foto
            {
                Id = 0,
                PathFoto = fotoPath,
                PathFotoKompresi = fotoPathKompresi,
            };

            var changeTracker = _appDbContext.FotoTable.Add(foto);

            await _appDbContext.SaveChangesAsync();

            return Ok(changeTracker.Entity);
        }

        public async Task<IActionResult> Hapus(int id, string? returnUrl)
        {
            returnUrl ??= Url.Action("Index");

            var foto = await _appDbContext.FotoTable
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

            if (foto == null)
            {
                _logger.LogError("Hapus Foto Gagal! Id {0} tidak ditemukan", id);
                return Redirect(returnUrl!);
            }

            _appDbContext.FotoTable.Remove(foto);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Hapus Foto Gagal! Simpan Database gagal! Ex : {0}", ex.ToString());
                return Redirect(returnUrl!);
            }

            try
            {
                if (System.IO.File.Exists(foto.PathFoto))
                    System.IO.File.Delete(foto.PathFoto);

                if (System.IO.File.Exists(foto.PathFotoKompresi))
                    System.IO.File.Delete(foto.PathFotoKompresi);
            }
            catch (Exception ex)
            {
                _logger.LogError("Hapus Foto Gagal! Hapus Foto di Storage Gagal! Ex : {0}", ex.ToString());
                return Redirect(returnUrl!);
            }

            return Redirect(returnUrl!);
        }

        private async Task<string> SaveFile(byte[] fileContent, string extension)
        {
            var folderPath = Path.GetFullPath(_webHostEnvironment.ContentRootPath + _photoFileSettingsOptions.FolderPath);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var fileName = $"{Path.GetRandomFileName()}{extension}";
            var fotoPath = folderPath + fileName;

            using (var fileStream = System.IO.File.Create(fotoPath))
            {
                await fileStream.WriteAsync(fileContent);
            }

            return fotoPath;
        }
    }
}
