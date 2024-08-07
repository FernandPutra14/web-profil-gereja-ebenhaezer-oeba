using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public static class FotoSizes
    {
        public const string Original = "original";
        public const string Small = "small";
        public const string Medium = "medium";
        public const string Large = "large";
    }

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

        public async Task<IActionResult> Index(int id, string size = FotoSizes.Original)
        {
            var foto = await _appDbContext.FotoTable
                .Where(f => f.Id == id).AsNoTracking()
                .FirstOrDefaultAsync();

            if (foto is null) return NotFound();

            var path = size switch
            {
                FotoSizes.Original => foto.PathFoto,
                FotoSizes.Small => foto.PathFotoSmall,
                FotoSizes.Medium => foto.PathFotoMedium,
                FotoSizes.Large => foto.PathFotoLarge,
                _ => string.Empty
            };

            if (string.IsNullOrEmpty(path)) return NotFound();

            var fullPath = Path.IsPathFullyQualified(path) ? path
                : _webHostEnvironment.ContentRootPath + "/" + path;

            if (!System.IO.File.Exists(fullPath)) return NotFound();

            var ext = Path.GetExtension(path).ToLowerInvariant().Remove(0, 1);
            return PhysicalFile(fullPath, $"image/{ext}");
        }
    }
}
