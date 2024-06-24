namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Home
{
    public class IndexVM
    {
        public int TotalAnak { get; set; }
        public int TotalRemaja { get; set; }
        public int TotalPemuda { get; set; }
        public int TotalDewasa { get; set; }
        public int TotalLansia { get; set; }

        public int TotalJemaat { get => TotalAnak + TotalRemaja + TotalPemuda + TotalDewasa + TotalLansia; }
    }
}
