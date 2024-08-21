using System.Net.Http.Headers;
using PKMGerejaEbenhaezer.Domain.Shared;

namespace PKMGerejaEbenhaezer.Web.Services.ScaniiApi;

public class ScaniiApiService : IScaniiApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ScaniiApiService> _logger;

    public ScaniiApiService(HttpClient httpClient, ILogger<ScaniiApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<Result<ScaniiApiResponse>> Files(IFormFile formFile)
    {
        try
        {
            var multipartContent = new MultipartFormDataContent();

            var streamContent = new StreamContent(formFile.OpenReadStream());
            streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(formFile.ContentType);

            multipartContent.Add(streamContent, "file");

            var response = await _httpClient.PostAsync("files", multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Request To Scanii Api Failed. Status Code : {@statusCode}", response.StatusCode);
                return new Error("ScaniiApiFiles.RequestFailed", "Error saat mencoba menghubungkan ke api anti-virus. Laporkan Error ke Admin");
            }

            var scaniiApiResponse = await response.Content.ReadFromJsonAsync<ScaniiApiResponse>();
            return scaniiApiResponse is null
                ? new Error("ScaniiApiFiles.DeserializeFailed", "Gagal deserialize httpContent")
                : scaniiApiResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Message : {@message}", ex.Message);
            return new Error("ScaniiApiFiles.RequestFailed", "Error saat mencoba menghubungkan ke api anti-virus. Laporkan Error ke Admin");
        }
    }
}