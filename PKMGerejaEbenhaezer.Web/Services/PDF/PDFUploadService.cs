using PKMGerejaEbenhaezer.Domain.Shared;
using PKMGerejaEbenhaezer.Web.Configurations;
using PKMGerejaEbenhaezer.Web.Services.FileHelper;

namespace PKMGerejaEbenhaezer.Web.Services.PDF
{
    public class PDFUploadService : IPDFUploadService
    {
        private readonly ILogger<PDFUploadService> _logger;
        private readonly PDFFileSettingsOptions _options;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileHelperService _fileHelperService;

        public PDFUploadService(PDFFileSettingsOptions options,
            ILogger<PDFUploadService> logger,
            IWebHostEnvironment webHostEnvironment,
            IFileHelperService fileHelperService)
        {
            _options = options;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _fileHelperService = fileHelperService;
        }

        public async Task<Result<string>> UploadAsync<T>(IFormFile formFile)
        {
            var folderPath = Path.GetFullPath(_webHostEnvironment.ContentRootPath + _options.FolderPath);

            try
            {
                EnsureDirectoryCreated(folderPath);
            }
            catch (Exception ex) 
            {
                _logger.LogError("Create Directory Failed. Exception : {0}", ex.ToString());

                return new Error("PDFUpload.UploadFailed", "Upload PDF Gagal");
            }

            var result = await _fileHelperService.ProcessFormFile<T>(
                formFile,
                new string[] { ".pdf" },
                _options.MinSizeLimit,
                _options.MaxSizeLimit);

            if (result.IsFailure) return Result<string>.Failure(result.Errors);

            var fileName = $"{Path.GetRandomFileName()}{Path.GetExtension(formFile.FileName)}";
            var pdfPath = folderPath + fileName;

            try
            {
                using (var fileStream = File.Create(pdfPath)) 
                {
                    await fileStream.WriteAsync(result.Value);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError("Saving PDF File to storage failed. Exception {0}", ex.ToString());

                return new Error("PDFUpload.UploadFailed", "Upload PDF Gagal");
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
