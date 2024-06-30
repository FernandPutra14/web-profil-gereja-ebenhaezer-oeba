namespace PKMGerejaEbenhaezer.Web.Configurations
{
    public class PDFFileSettingsOptions
    {
        public const string PDFFileSettings = "PDFFileSettings";

        public string FolderPath { get; set; } = string.Empty;
        public long MinSizeLimit { get; set; }
        public long MaxSizeLimit { get; set; }
    }
}
