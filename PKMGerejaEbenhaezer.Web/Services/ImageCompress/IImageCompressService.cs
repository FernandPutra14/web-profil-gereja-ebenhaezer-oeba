using PKMGerejaEbenhaezer.Domain.Shared;

namespace PKMGerejaEbenhaezer.Web.Services.ImageCompress
{
    public interface IImageCompressService
    {
        Task<Result<ImageCompressionResult>> Compress(byte[] image, string fileName);
    }
}
