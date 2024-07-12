namespace PKMGerejaEbenhaezer.Web.Services.ImageCompress
{
    public interface IImageCompressService
    {
        void Compress(byte[] image, string outputPath);
    }
}
