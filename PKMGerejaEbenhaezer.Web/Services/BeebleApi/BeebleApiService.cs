using Humanizer;
using PKMGerejaEbenhaezer.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PKMGerejaEbenhaezer.Web.Services.BeebleApi
{
    public class BeebleApiService : IBeebeleApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BeebleApiService> _logger;

        public BeebleApiService(HttpClient httpClient, ILogger<BeebleApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Book[]?> List()
        {
            var response = await _httpClient.GetFromJsonAsync<Dictionary<string, object?>>("list");

            if(response is null) return null;

            if(response.TryGetValue("data", out object? data))
                return data as Book[];

            return null;
        }

        public async Task<BeebleApiPassageResponse?> PassageContent(AyatAlkitab ayatAlkitab)
        {
            return await _httpClient.GetFromJsonAsync<BeebleApiPassageResponse?>(
                $"{ayatAlkitab.Kitab.Humanize()}/{ayatAlkitab.Pasal}:{string.Join(',' ,ayatAlkitab.Ayat)}?ver=tb");
        }
    }
}
