using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace PKMGerejaEbenhaezer.Web.Services.FileHelper
{
    public interface IFileHelperService
    {
        Task<byte[]> ProcessFormFile<T>(
            IFormFile formFile, 
            ModelStateDictionary modelState, 
            string[] permittedExtensions,
            long minSizeLimit,
            long maxSizeLimit);

        Task<byte[]> ProcessStreamedFile(
            MultipartSection section, ContentDispositionHeaderValue contentDisposition,
            ModelStateDictionary modelState, string[] permittedExtensions, long sizeLimit);
    }
}
