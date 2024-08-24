namespace PKMGerejaEbenhaezer.Web.Configurations;

public class ScaniiApiSettingsOptions
{
    public const string ScaniiApiSettings = "ScaniiApiSettings";

    public string ApiKey { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
}