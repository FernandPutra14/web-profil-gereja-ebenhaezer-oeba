using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Services.BeebleApi;

namespace PKMGerejaEbenhaezer.Web.Models.IbadahModels
{
    public class DetailVM
    {
        public Ibadah Ibadah { get; set; } = new();
        public BeebleApiResponse? NasPembimbing { get; set; }
        public BeebleApiResponse? Renungan { get; set; }
    }
}
