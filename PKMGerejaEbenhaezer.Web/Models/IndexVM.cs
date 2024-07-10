using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Models
{
    public class IndexVM<T>
    {
        private List<string> _monthName = new List<string>()
        {
            "Januari", "Febuari", "Maret", "April", "Mei", "Juni", "Juli",
            "Agustus", "September", "Oktober", "November", "Desember",
        };

        public PaginatedList<T> Items { get; set; } = PaginatedList<T>.Empty();

        public int? Tahun { get; set; }

        public int? Bulan { get; set; }

        public string? SearchString { get; set; }

        public string Title()
        {
            if (Bulan is null) return "";

            return $"/{MonthName(Bulan.Value).ToUpper()}";
        }

        public string FilterCssClass(int? filter = null)
        {
            if (Bulan is null && filter is null) return "active";

            if (filter == Bulan) return "active";

            return "";
        }

        public string MonthName(int month)
        {
            if (month < 1 || month > 12)
                throw new ArgumentOutOfRangeException(nameof(month), month, "{0} harus antara 1 dan 12");

            return _monthName[month - 1];
        }
    }
}
