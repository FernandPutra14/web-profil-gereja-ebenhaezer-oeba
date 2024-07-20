using Microsoft.AspNetCore.Mvc;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Utilities;
using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.FotoModels
{
    public class IndexVM
    {
        public PaginatedList<Foto>? Items { get; set; }

        [Required]
        public IFormFile FormFile { get; set; }
    }
}
