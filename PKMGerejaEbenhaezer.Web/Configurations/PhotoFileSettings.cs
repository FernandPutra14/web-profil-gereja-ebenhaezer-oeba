namespace PKMGerejaEbenhaezer.Web.Configurations
{
    public class PhotoFileSettings
    {
        public string[] PermittedFileExtensions { get; set; }
        public long SizeLimit { get; set; }
        public string FolderPath { get; set; }
    }
}
