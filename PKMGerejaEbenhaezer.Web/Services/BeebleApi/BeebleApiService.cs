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
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Dictionary<string, Book[]?>?>("list");

                if (response is null) return null;

                if (!response.TryGetValue("data", out Book[]? data)) return null;

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal memanggil api Alkitab {@message} {@date} {@exception}",
                    ex.Message, DateTime.Now, ex.ToString());

                return null;
            }
        }

        public async Task<BeebleApiResponse?> PassageContent(AyatAlkitab ayatAlkitab)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<Dictionary<string, BeebleApiResponse?>?>(
                $"{ayatAlkitab.Kitab.Humanize()}/{ayatAlkitab.Pasal}:{string.Join(',', ayatAlkitab.Ayat)}?ver=tb");

                if (response is null) return null;

                if (!response.TryGetValue("data", out BeebleApiResponse? data))
                    return null;

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gagal memanggil api Alkitab {@message} {@date} {@exception}",
                    ex.Message, DateTime.Now, ex.ToString());

                return null;
            }        
        }
    }
}
