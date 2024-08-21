using PKMGerejaEbenhaezer.Domain.Shared;

namespace PKMGerejaEbenhaezer.Web.Services.ScaniiApi;

public interface IScaniiApiService
{
    Task<Result<ScaniiApiResponse>> Files(IFormFile formFile);
}