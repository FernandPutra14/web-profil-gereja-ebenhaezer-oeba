using PKMGerejaEbenhaezer.Domain.Shared;
using PKMGerejaEbenhaezer.Web.Configurations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace PKMGerejaEbenhaezer.Web.Services.ImageCompress
{
    public class ImageCompressService : IImageCompressService
    {
        private readonly PhotoFileSettingsOptions _photoFileSettingsOptions;
        private readonly ImageCompressionOptions _imageCompressionOptions;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ImageCompressService> _logger;

        public ImageCompressService(PhotoFileSettingsOptions photoFileSettingsOptions,
            ImageCompressionOptions imageCompressionOptions,
            IWebHostEnvironment webHostEnvironment,
            ILogger<ImageCompressService> logger)
        {
            _photoFileSettingsOptions = photoFileSettingsOptions;
            _imageCompressionOptions = imageCompressionOptions;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<Result<ImageCompressionResult>> Compress(byte[] image, string fileName)
        {
            var compressionResult = new ImageCompressionResult();

            try
            {
                var folderPath = Path.GetFullPath(
                    _webHostEnvironment.ContentRootPath + _photoFileSettingsOptions.FolderPath);
                var encoder = new JpegEncoder { Quality = _imageCompressionOptions.CompressionQuality };

                compressionResult.SmallPath = $"{folderPath}{Path.GetFileNameWithoutExtension(fileName)}-small.jpeg";
                compressionResult.MediumPath = $"{folderPath}{Path.GetFileNameWithoutExtension(fileName)}-medium.jpeg";
                compressionResult.LargePath = $"{folderPath}{Path.GetFileNameWithoutExtension(fileName)}-large.jpeg";

                await Compress(image, compressionResult.SmallPath, _imageCompressionOptions.Small, encoder);
                await Compress(image, compressionResult.MediumPath, _imageCompressionOptions.Medium, encoder);
                await Compress(image, compressionResult.LargePath, _imageCompressionOptions.Large, encoder);

                return compressionResult;
            }
            catch (Exception ex)
            {
                if (File.Exists(compressionResult.SmallPath))
                    File.Delete(compressionResult.SmallPath);

                if (File.Exists(compressionResult.MediumPath))
                    File.Delete(compressionResult.MediumPath);

                if (File.Exists(compressionResult.LargePath))
                    File.Delete(compressionResult.LargePath);

                _logger.LogError(
                    ex,
                    "Exception when try to compress image. Message : {@message}. Timestamp : {@timeStamp}",
                    ex.Message,
                    DateTime.Now);

                return new Error("ImageCompressService.Compress", "Kompresi Foto Gagal");
            }
        }

        private static async Task Compress(
            byte[] image, 
            string outputPath, 
            System.Drawing.Size size, 
            IImageEncoder encoder)
        {
            using Image foto = Image.Load(image);

            var newSize = GetNewSize(foto.Size, size);

            foto.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = newSize,
                Mode = ResizeMode.Stretch,
                Sampler = KnownResamplers.Bicubic
            }));

            await foto.SaveAsync(outputPath, encoder);
        }

        private static Size GetNewSize(Size original, System.Drawing.Size maxSize)
        {
            if (original.Height <= maxSize.Height && original.Width <= maxSize.Width)
                return original;

            var ratioX = (double)maxSize.Width / original.Width;
            var ratioY = (double)maxSize.Height / original.Height;

            var ratio = Math.Min(ratioX, ratioY);

            return new((int)(original.Width * ratio), (int)(original.Height * ratio));
        }
    }
}
