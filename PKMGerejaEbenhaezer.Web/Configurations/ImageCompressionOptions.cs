using System.Drawing;

namespace PKMGerejaEbenhaezer.Web.Configurations;

public class ImageCompressionOptions
{
    public const string ImageCompression = "ImageCompression";

    public int CompressionQuality {  get; set; }
    public Size Small {  get; set; } 
    public Size Medium {  get; set; } 
    public Size Large {  get; set; } 
}
