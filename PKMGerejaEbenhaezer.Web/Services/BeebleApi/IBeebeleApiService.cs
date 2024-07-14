using PKMGerejaEbenhaezer.Domain.ValueObjects;

namespace PKMGerejaEbenhaezer.Web.Services.BeebleApi
{
    public interface IBeebeleApiService
    {
        Task<Book[]?> List();
        Task<BeebleApiPassageResponse?> PassageContent(AyatAlkitab ayatAlkitab);
    }
}
