using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Web.Utlities;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class FileController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> FotoKetuaRayon(int id)
        {
            //Validasi id rayon
            var rayon = await _appDbContext.RayonTable.Where(r => r.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if(rayon == null)
                return NotFound();

            return File(rayon.FotoKetua, "image/jpg", $"Foto {rayon.KetuaRayon}");
        }
    }
}
