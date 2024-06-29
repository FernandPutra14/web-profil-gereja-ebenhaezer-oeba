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

            if(foto == null)
                return NotFound();

            var path = Path.IsPathFullyQualified(foto.PathFoto) ? foto.PathFoto
                : _webHostEnvironment.ContentRootPath + foto.PathFoto;

            if (System.IO.File.Exists(path) == false)
                return NotFound();

            var ext = Path.GetExtension(foto.PathFoto).ToLowerInvariant().Remove(0, 1);
            return PhysicalFile(path, $"image/{ext}");
        }

        public async Task<IActionResult> FotoKompresi(int index)
        {
            var foto = await _appDbContext.FotoTable
                .Where(f => f.Id == index).AsNoTracking()
                .FirstOrDefaultAsync();

            if (foto == null)
                return NotFound();

            if (System.IO.File.Exists(foto.PathFotoKompresi) == false)
                return NotFound();

            var ext = Path.GetExtension(foto.PathFotoKompresi).ToLowerInvariant().Remove(0, 1);
            return PhysicalFile(foto.PathFotoKompresi, $"image/{ext}");
        }
    }
}
