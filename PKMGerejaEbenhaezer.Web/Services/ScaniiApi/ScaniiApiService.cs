using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using PKMGerejaEbenhaezer.Domain.Shared;

namespace PKMGerejaEbenhaezer.Web.Services.ScaniiApi;

public class ScaniiApiService : IScaniiApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ScaniiApiService> _logger;
    private readonly IMemoryCache _memoryCache;

    public ScaniiApiService(
        HttpClient httpClient, 
        ILogger<ScaniiApiService> logger, 
        IMemoryCache memoryCache)
    {
        _httpClient = httpClient;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    public async Task<Result<ScaniiApiResponse>> Files(IFormFile formFile)
    {
        try
        {
            using var formFileStream = formFile.OpenReadStream();
            var hash = await SHA1.HashDataAsync(formFileStream);
            var key = Encoding.UTF8.GetString(hash);

            if (_memoryCache.TryGetValue(key, out ScaniiApiResponse? cache)) return cache!;

            var multipartContent = new MultipartFormDataContent();

            var streamContent = new StreamContent(formFileStream);
            streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(formFile.ContentType);

            multipartContent.Add(streamContent, "file");

            var response = await _httpClient.PostAsync("files", multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Request To Scanii Api Failed. Status Code : {@statusCode}", response.StatusCode);
                return new Error("ScaniiApiFiles.RequestFailed", "Error saat mencoba menghubungkan ke api anti-virus. Laporkan Error ke Admin");
            }

            var scaniiApiResponse = await response.Content.ReadFromJsonAsync<ScaniiApiResponse>();

            if (scaniiApiResponse is null) return new Error("ScaniiApiFiles.DeserializeFailed", "Gagal deserialize httpContent");

            _memoryCache.Set(key, scaniiApiResponse, TimeSpan.FromMinutes(1));

            return scaniiApiResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Message : {@message}", ex.Message);
            return new Error("ScaniiApiFiles.RequestFailed", "Error saat mencoba menghubungkan ke api anti-virus. Laporkan Error ke Admin");
        }
    }
}