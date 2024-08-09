using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.FotoModels;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Services.FileHelper;
using PKMGerejaEbenhaezer.Web.Services.ImageCompress;
using PKMGerejaEbenhaezer.Web.Services.ToastrNotification;
using PKMGerejaEbenhaezer.Web.Utilities;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class FotoController : Controller
    {
        private readonly IAppDbContext _appDbContext;
        private readonly PhotoFileSettingsOptions _photoFileSettingsOptions;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FotoController> _logger;
        private readonly IToastrNotificationService _notificationService;
        private readonly IImageCompressService _imageCompressService;
        private readonly IFileHelperService _fileHelperService;

        public FotoController(IAppDbContext appDbContext,
            PhotoFileSettingsOptions photoFileSettingsOptions,
            IWebHostEnvironment webHostEnvironment,
            ILogger<FotoController> logger,
            IToastrNotificationService notificationService,
            IImageCompressService imageCompressService,
            IFileHelperService fileHelperService)
        {
            _appDbContext = appDbContext;
            _photoFileSettingsOptions = photoFileSettingsOptions;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
            _notificationService = notificationService;
            _imageCompressService = imageCompressService;
            _fileHelperService = fileHelperService;
        }

        public async Task<IActionResult> Index(int? pageIndex = null)
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
        public async Task<IActionResult> Tambah(IndexVM indexVM, bool isJson = false, int pageIndex = 1)
        {
            if(!isJson)
            {
                var daftarFoto = await _appDbContext.FotoTable
                .Include(f => f.Pembuat)
                .OrderByDescending(f => f.TanggalDiBuat)
                .AsNoTracking().ToListAsync();

                var paginatedList = PaginatedList<Foto>.Create(daftarFoto, pageIndex, 16);

                indexVM.Items = paginatedList;
            }

            //Validasi
            if (!ModelState.IsValid)
                return isJson ? BadRequest(ModelState) : View(nameof(Index), indexVM);

            //Upload File
            var processFormFileResult = await _fileHelperService.ProcessFormFile<IFormFile>(
                indexVM.FormFile,
                _photoFileSettingsOptions.PermittedFileExtensions,
                _photoFileSettingsOptions.MinSizeLimit,
                _photoFileSettingsOptions.MaxSizeLimit);

            if (processFormFileResult.IsFailure)
            {
                ModelState.AddModelError(nameof(IndexVM.FormFile), processFormFileResult.Error.Message);

                return isJson ? BadRequest(ModelState) : View(nameof(Index), indexVM);
            }

            var fotoPath = string.Empty;

            try
            {
                fotoPath = await SaveFile(processFormFileResult.Value, Path.GetExtension(indexVM.FormFile.FileName));
            }
            catch (Exception ex)
            {
                _logger.LogError("Upload Foto Gagal. Error: {0}", ex.ToString());

                if (!isJson)
                {
                    _notificationService.AddNotification(
                    new ToastrNotification
                    {
                        Type = ToastrNotificationType.Error,
                        Title = "Upload Foto Gagal",
                        Message = "Gagal menyimpan foto. Laporkan error ke administrator",
                    });
                }

                if(System.IO.File.Exists(fotoPath))
                    System.IO.File.Delete(fotoPath);

                return isJson ? StatusCode(StatusCodes.Status500InternalServerError): View(nameof(Index), indexVM);
            }

            var compressResult = await _imageCompressService.Compress(
                processFormFileResult.Value,
                Path.GetFileName(fotoPath));

            if (compressResult.IsFailure)
            {
                if (!isJson)
                {
                    _notificationService.AddNotification(new ToastrNotification
                    {
                        Type = ToastrNotificationType.Error,
                        Title = compressResult.Error.Message,
                        Message = "Gagal Compress Foto. Laporkan error ke administrator"
                    });
                }

                if (System.IO.File.Exists(fotoPath))
                    System.IO.File.Delete(fotoPath);

                return isJson ? StatusCode(StatusCodes.Status500InternalServerError) : View(nameof(Index), indexVM);
            }

            var foto = new Foto
            {
                Id = 0,
                PathFoto = fotoPath,
                PathFotoSmall = compressResult.Value.SmallPath,
                PathFotoMedium = compressResult.Value.MediumPath,
                PathFotoLarge = compressResult.Value.LargePath,
            };

            _appDbContext.FotoTable.Add(foto);

            try
            {
                await _appDbContext.SaveChangesAsync();

                if (!isJson)
                {
                    _notificationService.AddNotification(
                    new ToastrNotification
                    {
                        Type = ToastrNotificationType.Success,
                        Title = "Upload Foto Berhasil",
                        Message = "Foto berhasil disimpan",
                    });
                }

                return isJson ? Ok(foto) : RedirectToAction(nameof(Index), new { pageIndex });
            }
            catch (Exception ex)
            {
                if (System.IO.File.Exists(fotoPath))
                    System.IO.File.Delete(fotoPath);

                if (System.IO.File.Exists(compressResult.Value.SmallPath))
                    System.IO.File.Delete(compressResult.Value.SmallPath);

                if (System.IO.File.Exists(compressResult.Value.MediumPath))
                    System.IO.File.Delete(compressResult.Value.MediumPath);

                if (System.IO.File.Exists(compressResult.Value.LargePath))
                    System.IO.File.Delete(compressResult.Value.LargePath);

                _logger.LogError("Upload Foto Gagal. Error: {0}", ex.ToString());

                if (!isJson)
                {
                    _notificationService.AddNotification(
                    new ToastrNotification
                    {
                        Type = ToastrNotificationType.Error,
                        Title = "Upload Foto Gagal",
                        Message = "Gagal menyimpan foto. Laporkan error ke administrator",
                    });
                }

                return isJson ? StatusCode(StatusCodes.Status500InternalServerError) : View(nameof(Index), indexVM);
            }
        }

        public async Task<IActionResult> Hapus(int id, string? returnUrl)
        {
            returnUrl ??= Url.Action("Index");

            var foto = await _appDbContext.FotoTable
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();

            if (foto is null) return NotFound();

            _appDbContext.FotoTable.Remove(foto);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Hapus Foto Gagal! Simpan Database gagal! Ex : {0}", ex.ToString());
                _notificationService.AddNotification(
                    new ToastrNotification
                    {
                        Type = ToastrNotificationType.Error,
                        Title = "Hapus Foto Gagal!",
                        Message = "Hapus dari database gagal"
                    }
                );
                return Redirect(returnUrl!);
            }

            try
            {
                if (System.IO.File.Exists(foto.PathFoto))
                    System.IO.File.Delete(foto.PathFoto);

                if (System.IO.File.Exists(foto.PathFotoSmall))
                    System.IO.File.Delete(foto.PathFotoSmall);

                if (System.IO.File.Exists(foto.PathFotoMedium))
                    System.IO.File.Delete(foto.PathFotoMedium);

                if (System.IO.File.Exists(foto.PathFotoLarge))
                    System.IO.File.Delete(foto.PathFotoLarge);
            }
            catch (Exception ex)
            {
                _logger.LogError("Hapus Foto Gagal! Hapus Foto di Storage Gagal! Ex : {0}", ex.ToString());
                _notificationService.AddNotification(
                    new ToastrNotification
                    {
                        Type = ToastrNotificationType.Error,
                        Title = "Hapus Foto Gagal!",
                        Message = "Hapus di penyimpanan gagal"
                    }
                );
                return Redirect(returnUrl!);
            }

            _notificationService.AddNotification(
                    new ToastrNotification
                    {
                        Type = ToastrNotificationType.Success,
                        Title = "Hapus Foto Berhasil!",
                        Message = "Foto telah dihapus"
                    }
                );
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
