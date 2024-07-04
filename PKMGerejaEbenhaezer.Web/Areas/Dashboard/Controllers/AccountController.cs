using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Account;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AccountController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var daftarUser = await _appDbContext.AppUserTable
                .AsNoTracking().ToListAsync();

            return View(daftarUser ?? new());
        }

        //Tambah Akun
        public IActionResult Tambah()
        {
            return View(new TambahVM());
        }

        //Ubah password


        //Ubah user name
         

    }
}
