using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class PengumumanController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public PengumumanController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Dokumen(int id)
        {
            var pengumuman = await _appDbContext.PengumumanTable.Where(p => p.Id == id)
                .AsNoTracking().FirstOrDefaultAsync();

            if (pengumuman is null) return NotFound();

            if (!pengumuman.HaveDocument) return BadRequest();

            if (!System.IO.File.Exists(pengumuman.PathPDF)) return NotFound();

            return PhysicalFile(pengumuman.PathPDF, "application/pdf");
        }
    }
}
