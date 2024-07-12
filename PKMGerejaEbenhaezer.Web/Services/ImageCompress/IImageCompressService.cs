namespace PKMGerejaEbenhaezer.Web.Services.ImageCompress
{
    public interface IImageCompressService
    {
        Task Compress(byte[] image, string outputPath);
    }
}
