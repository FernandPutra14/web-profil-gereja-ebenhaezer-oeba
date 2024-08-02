using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using PKMGerejaEbenhaezer.Domain.Shared;

namespace PKMGerejaEbenhaezer.Web.Services.FileHelper
{
    public interface IFileHelperService
    {
        Task<Result<byte[]>> ProcessFormFile<T>(
            IFormFile formFile,
            string[] permittedExtensions,
            long minSizeLimit,
            long maxSizeLimit);

        Task<Result<byte[]>> ProcessStreamedFile(
            MultipartSection section, ContentDispositionHeaderValue contentDisposition,
            string[] permittedExtensions, long sizeLimit);
    }
}
