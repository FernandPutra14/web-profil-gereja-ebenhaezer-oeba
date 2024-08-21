namespace PKMGerejaEbenhaezer.Web.Services.ScaniiApi;

public class ScaniiApiResponse
{
    public string Id { get; set; } = string.Empty;
    public string Checksum { get; set; } = string.Empty;
    public int ContentLength { get; set; }
    public string[] Findings { get; set; } = Array.Empty<string>();
    public string ContentType { get; set; } = string.Empty;
    public string CreationDate { get; set; } = string.Empty;
    public Dictionary<string, string>? MetaData { get; set; }
    public string? Error { get; set; }
}
