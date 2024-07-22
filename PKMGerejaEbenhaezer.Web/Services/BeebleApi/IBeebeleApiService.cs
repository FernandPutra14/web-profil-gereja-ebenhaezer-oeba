using PKMGerejaEbenhaezer.Domain.ValueObjects;

namespace PKMGerejaEbenhaezer.Web.Services.BeebleApi
{
    public interface IBeebeleApiService
    {
        Task<Book[]?> List();
        Task<BeebleApiResponse?> PassageContent(AyatAlkitab ayatAlkitab);
        Task<bool> IsValid(AyatAlkitab ayatAlkitab);
    }
}
