using Humanizer;
using Microsoft.Extensions.Caching.Memory;
using PKMGerejaEbenhaezer.Domain.ValueObjects;

namespace PKMGerejaEbenhaezer.Web.Services.BeebleApi
{
    public class BeebleApiService : IBeebeleApiService
    {
        private const string ListCacheKey = "ListCacheKey";

        private readonly HttpClient _httpClient;
        private readonly ILogger<BeebleApiService> _logger;
        private readonly IMemoryCache _memoryCache;

        public BeebleApiService(HttpClient httpClient, ILogger<BeebleApiService> logger, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public async Task<bool> IsValid(AyatAlkitab ayatAlkitab)
        {
            var passageList = await List();

            if (passageList is null) return false;

            //Validasi Kitab
            var book = passageList.FirstOrDefault(b => b.Name == ayatAlkitab.Kitab.Humanize());

            if(book is null) return false;

            //Validasi Pasal
            if (ayatAlkitab.Pasal > book.Chapter || ayatAlkitab.Pasal <= 0) return false;

            //Validasi Ayat
            if (ayatAlkitab.Ayat.Length > 0)
            {
                if (ayatAlkitab.Ayat.Min() <= 0) return false;

                var response = await PassageContent(
                    new AyatAlkitab(ayatAlkitab.Kitab, ayatAlkitab.Pasal, Array.Empty<int>()));

                if(response is null) return false;

                if(ayatAlkitab.Ayat.Max() > response.Verses.Where(v => v.Type != "title").Count())
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<Book[]?> List()
        {
            if (_memoryCache.TryGetValue(ListCacheKey, out Book[]? cacheData)) return cacheData;

            try
            {
                var response = await _httpClient.GetFromJsonAsync<Dictionary<string, Book[]?>?>("list");

                if (response is null) return null;

                if (!response.TryGetValue("data", out Book[]? data)) return null;

                _memoryCache.Set(ListCacheKey, data);

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
