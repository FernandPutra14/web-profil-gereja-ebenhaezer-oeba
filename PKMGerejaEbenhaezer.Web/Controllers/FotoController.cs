using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Web.Configurations;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class FotoController : Controller
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<FotoController> _logger;

        public FotoController(IAppDbContext appDbContext,
            IWebHostEnvironment webHostEnvironment,
            ILogger<FotoController> logger)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int id, bool kompresi = false)
        {
            var foto = await _appDbContext.FotoTable
                .Where(f => f.Id == id).AsNoTracking()
                .FirstOrDefaultAsync();

            if (foto is null)
            {
                _logger.LogError("Foto dengan Id {0} tidak ditemukan di database", id);
                return NotFound();
            }

            var path = kompresi ? foto.PathFotoKompresi : foto.PathFoto;

            var fullPath = Path.IsPathFullyQualified(path) ? path
                : _webHostEnvironment.ContentRootPath + "/" + path;

            if (System.IO.File.Exists(fullPath) == false)
            {
                _logger.LogError("File dengan path {0} tidak ditemukan", fullPath);
                return NotFound();
            }

            var ext = Path.GetExtension(path).ToLowerInvariant().Remove(0, 1);
            return PhysicalFile(fullPath, $"image/{ext}");
        }
    }
}
