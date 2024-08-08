using PKMGerejaEbenhaezer.Domain.Entity;

namespace PKMGerejaEbenhaezer.Web.Models;

public class FotoPickerVM
{
    public string Id { get; set; } = string.Join("", Guid.NewGuid().ToString().Split('-'));
    public List<Foto> DaftarFoto { get; set; } = new();
    public bool Multi { get; set; }
    public string InputName { get; set; } = string.Empty;
    public List<int>? DaftarIdFoto { get; set; }
}