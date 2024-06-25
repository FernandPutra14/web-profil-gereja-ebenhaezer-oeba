namespace PKMGerejaEbenhaezer.Web.Configurations
{
    public class PhotoFileSettingsOptions
    {
        public const string PhotoFileSettings = "PhotoFileSettings";

        public string[] PermittedFileExtensions { get; set; } = Array.Empty<string>();
        public long SizeLimit { get; set; }
        public string FolderPath { get; set; } = string.Empty;
    }
}
