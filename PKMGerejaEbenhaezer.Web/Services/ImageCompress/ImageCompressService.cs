using PKMGerejaEbenhaezer.Web.Configurations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

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

                    var maxSize = _photoFileSettingsOptions.CompressionMaxSize;
                    var originalSize = fotoKompresi.Size;

                    if(originalSize.Height > maxSize.Height || 
                        originalSize.Width > maxSize.Width)
                    {
                        var ratioX = (double)maxSize.Width / originalSize.Width;
                        var ratioY = (double)maxSize.Height / originalSize.Height;

                        var ratio = Math.Min(ratioX, ratioY);

                        var newSize = new Size((int)(originalSize.Width * ratio), 
                            (int)(originalSize.Height * ratio));

                        fotoKompresi.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = newSize,
                            Mode = ResizeMode.Stretch,
                        }));
                    }

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
