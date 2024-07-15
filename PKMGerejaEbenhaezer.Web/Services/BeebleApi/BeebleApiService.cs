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
            var response = await _httpClient.GetFromJsonAsync<Dictionary<string, Book[]?>?>("list");

            if (response is null) return null;

            if (!response.TryGetValue("data", out Book[]? data)) return null;

            return data;
        }

        public async Task<BeebleApiResponse?> PassageContent(AyatAlkitab ayatAlkitab)
        {
            var response = await _httpClient.GetFromJsonAsync<Dictionary<string, BeebleApiResponse?>?>(
                $"{ayatAlkitab.Kitab.Humanize()}/{ayatAlkitab.Pasal}:{string.Join(',' ,ayatAlkitab.Ayat)}?ver=tb");

            if (response is null) return null;

            if(!response.TryGetValue("data", out BeebleApiResponse? data))
                return null;

            return data;
        }
    }
}
