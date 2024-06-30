
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Services.PDF
{
    public class PDFUploadService : IPDFUploadService
    {
        private readonly ILogger<PDFUploadService> _logger;
        private readonly PDFFileSettingsOptions _options;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PDFUploadService(PDFFileSettingsOptions options,
            ILogger<PDFUploadService> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _options = options;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string?> UploadAsync<T>(ModelStateDictionary modelState, IFormFile formFile)
        {
            var folderPath = Path.GetFullPath(_webHostEnvironment.ContentRootPath + _options.FolderPath);

            try
            {
                EnsureDirectoryCreated(folderPath);
            }
            catch (Exception ex) 
            {
                modelState.AddModelError(string.Empty, "Upload PDF Gagal!");
                _logger.LogError("Create Directory Failed. Exception : {0}", ex.ToString());
                return null;
            }

            var fileFormContent = await FileHelpers.ProcessFormFile<T>(
                formFile,
                modelState,
                new string[] { ".pdf" },
                _options.MinSizeLimit,
                _options.MaxSizeLimit);

            if (!modelState.IsValid) return null;

            var fileName = $"{Path.GetRandomFileName()}{Path.GetExtension(formFile.FileName)}";
            var pdfPath = folderPath + fileName;

            try
            {
                using (var fileStream = File.Create(pdfPath)) 
                {
                    await fileStream.WriteAsync(fileFormContent);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError("Saving PDF File to storage failed. Exception {0}", ex.ToString());
                modelState.AddModelError(string.Empty, "Upload PDF Gagal!");
                return null;
            }

            return pdfPath;
        }

        private void EnsureDirectoryCreated(string folderPath)
        {
            _logger.LogInformation("Folder Path : {0}", folderPath);
            if (!Directory.Exists(folderPath))
            {
                _logger.LogInformation("Folder Path Not Exist. Create Directory");

                Directory.CreateDirectory(folderPath);
            }
        }
    }
}
