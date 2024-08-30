using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Web.Models.Home;

namespace PKMGerejaEbenhaezer.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAppDbContext _appDbContext;

    public HomeController(ILogger<HomeController> logger,
        IAppDbContext appDbContext)
    {
        _logger = logger;
        _appDbContext = appDbContext;
    }

    [OutputCache]
    public async Task<IActionResult> Index()
    {
        var daftarPengumuman = await _appDbContext.PengumumanTable
            .OrderByDescending(p => p.TanggalDiBuat)
            .Include(p => p.Foto)
            .Include(p => p.Pembuat)
            .Take(3)
            .AsNoTracking().ToListAsync();

        var daftarWarta = await _appDbContext.WartaJemaatTable
            .OrderByDescending(p => p.TanggalWarta)
            .Include(w => w.Pembuat)
            .Take(3)
            .AsNoTracking().ToListAsync();

        var daftarPendeta = await _appDbContext.PendetaTable
            .Include(p => p.Foto)
            .AsNoTracking().ToListAsync();

        var daftarIbadah = await _appDbContext.IbadahTable
            .Include(i => i.Pendeta).ThenInclude(p => p.Foto)
            .Include(i => i.KategoriIbadah)
            .Where(i => i.Pendeta != null && i.KategoriIbadah != null)
            .OrderByDescending(i => i.TanggalIbadah)
            .Take(3).AsNoTracking().ToListAsync();

        return View(new IndexVM
        {
            DaftarPengumuman = daftarPengumuman,
            DaftarWartaJemaat = daftarWarta,
            DaftarPendeta = daftarPendeta,
            DaftarIbadah = daftarIbadah
        });
    }

    [OutputCache]
    public IActionResult Kontak()
    {
        return View();
    }

    [OutputCache]
    public IActionResult SejarahGereja()
    { 
        return View(); 
    }

    [OutputCache]
    public IActionResult VisiMisi()
    {
        return View();
    }

    [OutputCache]
    public async Task<IActionResult> KoordinatorRayon()
    {
        var daftarRayon = await _appDbContext.RayonTable
            .OrderBy(x => x.Id)
            .Include(x => x.FotoKetua)
            .ToListAsync();

        return View(new KoordinatorRayonVM
        {
            DaftarRayon = daftarRayon
        });
    }

    public IActionResult ProblemBadRequest()
    {
        return BadRequest();
    }

    public IActionResult InternalServerError()
    {
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Any)]
    public IActionResult StatusCode404()
    {
        return View(); 
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Any)]
    public IActionResult StatusCode400()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Any)]
    public IActionResult StatusCode500()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Any)]
    public IActionResult Error()
    {
        var exceptionHandlerFeature = HttpContext.Features.GetRequiredFeature<IExceptionHandlerPathFeature>();

        var error = exceptionHandlerFeature.Error;
        var path = exceptionHandlerFeature.Path;

        _logger.LogError(
            error,
            "Unhandled Exception. Message : {@message}, Timestamp : {@dateTime}, Path : {@path}",
            error.Message,
            DateTime.Now,
            path);

        return StatusCode(StatusCodes.Status500InternalServerError);
    }
}
