using Emgu.CV;
using Emgu.CV.CvEnum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Utlities;

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
                _photoFileSettingsOptions.SizeLimit);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fotoPath = string.Empty;
            var fotoPathKompresi = string.Empty;
            try
            {
                fotoPath = await SaveFile(fileFormContent, Path.GetExtension(formFile.FileName));
                fotoPathKompresi = fotoPath;
                //fotoPathKompresi = Path.Combine(Path.GetDirectoryName(fotoPath)!,
                //    $"{Path.GetFileNameWithoutExtension(fotoPath)}-kompresi{Path.GetExtension(fotoPath)}");

                //using (Mat original = CvInvoke.Imread(fotoPath))
                //{
                //    CvInvoke.Imwrite(fotoPathKompresi, original, KeyValuePair.Create(ImwriteFlags.JpegQuality, 75));
                //}
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
                PathFotoKompresi = fotoPath,
            };

            var changeTracker = _appDbContext.FotoTable.Add(foto);

            await _appDbContext.SaveChangesAsync();

            return Ok(changeTracker.Entity);
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
