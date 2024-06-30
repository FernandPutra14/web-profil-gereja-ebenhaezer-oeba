using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PKMGerejaEbenhaezer.Web.Services.PDF
{
    public interface IPDFUploadService
    {
        Task<string?> UploadAsync<T>(ModelStateDictionary ModelState, IFormFile formFile);
    }
}
