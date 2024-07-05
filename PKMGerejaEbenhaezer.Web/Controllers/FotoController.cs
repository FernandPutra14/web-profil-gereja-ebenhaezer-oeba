using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
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

        public async Task<IActionResult> Index(int id)
        {
            var foto = await _appDbContext.FotoTable
                .Where(f => f.Id == id).AsNoTracking()
                .FirstOrDefaultAsync();

            if (foto == null)
            {
                _logger.LogError("Foto dengan Id {0} tidak ditemukan di database", id);
                return NotFound();
            }

            _logger.LogInformation(_webHostEnvironment.ContentRootPath);

            var path = Path.IsPathFullyQualified(foto.PathFoto) ? foto.PathFoto
                : _webHostEnvironment.ContentRootPath + "/" + foto.PathFoto;

            _logger.LogInformation(path);

            if (System.IO.File.Exists(path) == false)
            {
                _logger.LogError("File dengan path {0} tidak ditemukan", path);
                return NotFound();
            }

            var ext = Path.GetExtension(foto.PathFoto).ToLowerInvariant().Remove(0, 1);
            return PhysicalFile(path, $"image/{ext}");
        }

        public async Task<IActionResult> FotoKompresi(int id)
        {
            var foto = await _appDbContext.FotoTable
                .Where(f => f.Id == id).AsNoTracking()
                .FirstOrDefaultAsync();

            if (foto == null)
            {
                _logger.LogError("Foto dengan Id {0} tidak ditemukan di database", id);
                return NotFound();
            }

            _logger.LogInformation(_webHostEnvironment.ContentRootPath);

            var path = Path.IsPathFullyQualified(foto.PathFotoKompresi) ? foto.PathFotoKompresi
                : _webHostEnvironment.ContentRootPath + "/" + foto.PathFotoKompresi;

            _logger.LogInformation(path);

            if (System.IO.File.Exists(path) == false)
            {
                _logger.LogError("File dengan path {0} tidak ditemukan", path);
                return NotFound();
            }

            var ext = Path.GetExtension(path).ToLowerInvariant().Remove(0, 1);
            return PhysicalFile(path, $"image/{ext}");
        }
    }
}
