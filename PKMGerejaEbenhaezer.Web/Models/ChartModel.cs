using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Models
{
    public class ChartModel
    {
        public string Id { get; } = $"chart{Guid.NewGuid()}";
        public Dictionary<string, double> Data { get; set; } = new Dictionary<string, double>();
        public string Label { get; set; } = string.Empty;
        public ChartType Type { get; set; }
    }

    public enum ChartType
    {
        [Display(Description = "doughnut", Name = "doughnut")]
        Doughnut,

        [Display(Description = "pie", Name = "pie")]
        Pie,

        [Display(Description = "polarArea", Name = "polarArea")]
        PolarArea,

        [Display(Description = "line", Name = "line")]
        Line,

        [Display(Description = "bar", Name = "bar")]
        Bar
    }
}
