
using PKMGerejaEbenhaezer.Web.Configurations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace PKMGerejaEbenhaezer.Web.Services.ImageCompress
{
    public class ImageCompressService : IImageCompressService
    {
        private readonly PhotoFileSettingsOptions _photoFileSettingsOptions;
        private readonly ILogger<ImageCompressService> _logger;

        public ImageCompressService(PhotoFileSettingsOptions photoFileSettingsOptions, 
            ILogger<ImageCompressService> logger)
        {
            _photoFileSettingsOptions = photoFileSettingsOptions;
            _logger = logger;
        }

        public async Task Compress(byte[] image, string outputPath)
        {
            try
            {
                using (var fotoKompresi = Image.Load(image))
                {
                    var encoder = new JpegEncoder
                    {
                        Quality = _photoFileSettingsOptions.CompressionQuality
                    };
                    await fotoKompresi.SaveAsync(outputPath, encoder);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Compress gambar gagal. Exception : {0}", ex.ToString());
                throw;
            }
        }
    }
}
