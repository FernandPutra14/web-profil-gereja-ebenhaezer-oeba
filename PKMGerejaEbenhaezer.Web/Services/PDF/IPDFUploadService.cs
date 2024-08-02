using Microsoft.AspNetCore.Mvc.ModelBinding;
using PKMGerejaEbenhaezer.Domain.Shared;

namespace PKMGerejaEbenhaezer.Web.Services.PDF
{
    public interface IPDFUploadService
    {
        Task<Result<string>> UploadAsync<T>(IFormFile formFile);
    }
}
