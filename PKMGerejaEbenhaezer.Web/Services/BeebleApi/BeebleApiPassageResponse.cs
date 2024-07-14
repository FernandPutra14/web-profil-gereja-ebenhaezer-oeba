namespace PKMGerejaEbenhaezer.Web.Services.BeebleApi
{
    public class BeebleApiPassageResponse
    {
        public Data Data { get; set; }
    }

    public class BeebleApiListResponse
    {
        Book[] Data { get; set; }
    }

    public class Data
    {
        public Book Book { get; set; } = new();
        public VerseResponse[] Verses { get; set; } = Array.Empty<VerseResponse>();
    }

    public class VerseResponse
    {
        public int Verse { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class Book
    {
        public int No { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbr { get; set; } = string.Empty;
        public int Chapter { get; set; }
    }
}
