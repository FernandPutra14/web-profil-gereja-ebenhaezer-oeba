namespace PKMGerejaEbenhaezer.Web.Models
{
    public class ChartModel
    {
        public Dictionary<string, double> Data { get; set; } = new Dictionary<string, double>();
        public string Label { get; set; } = string.Empty;
        public ChartType Type { get; set; }
    }

    public enum ChartType
    {
        Doughnut, Bar, PolarArea
    }

    public static class ChartTypeExtension
    {
        public static string ToStringJS(this ChartType chartType)
        {
            switch (chartType)
            {
                case ChartType.Doughnut:
                    return "doughnut";
                case ChartType.Bar:
                    return "bar";
                case ChartType.PolarArea:
                    return "polarArea";
            };

            return string.Empty;
        }
    }
}
