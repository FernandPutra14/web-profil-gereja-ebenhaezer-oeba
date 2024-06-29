using Microsoft.AspNetCore.Mvc;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Utlities;
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
