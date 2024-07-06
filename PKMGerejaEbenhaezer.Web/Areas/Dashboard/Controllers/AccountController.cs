using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Account;

namespace PKMGerejaEbenhaezer.Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize(Roles = "Super Admin")]
    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AccountController> _logger;

        public AccountController(AppDbContext appDbContext, ILogger<AccountController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
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

        [HttpPost]
        public async Task<IActionResult> Tambah(TambahVM tambahVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(tambahVM);

            var duplikasiNama = await _appDbContext.AppUserTable
                .AnyAsync(u => u.UserName.ToLower() == tambahVM.UserName.ToLower());

            if (duplikasiNama)
            {
                ModelState.AddModelError(nameof(TambahVM.UserName), "User Name sudah digunakan!");
                return View(tambahVM);
            }

            //Simpan ke database
            var hasher = new PasswordHasher<AppUser>();
            var appUser = new AppUser
            {
                Id = 0,
                UserName = tambahVM.UserName,
                PasswordHash = hasher.HashPassword(null, tambahVM.Password),
                Role = AppUserRoles.Admin,
            };
            _appDbContext.AppUserTable.Add(appUser);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState
                    .AddModelError(string.Empty, "Gagal menyimpan ke database. Silahkan hubungi administrator!");
                _logger.LogError("Tambah Akun. Exception : {0}", ex.ToString());
                return View(tambahVM);
            }

            return RedirectToAction(nameof(Index));
        }

        //Hapus Akun
        [HttpPost]
        public async Task<IActionResult> Hapus(int id)
        {
            var user = await _appDbContext.AppUserTable
                .Where(a => a.Id == id).FirstOrDefaultAsync();

            if (user is null) return NotFound();

            if (user.UserName == User.Identity?.Name)
            {
                _logger.LogError("Mencoba menghapus akun sendiri");
                return BadRequest();
            }

            if (user.Role == AppUserRoles.SuperAdmin)
            {
                _logger.LogError("Mencoba menghapus akun super admin");
                return BadRequest();
            }

            _appDbContext.AppUserTable.Remove(user);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("Hapus. Error saat menyimpan. Exception : {0}", ex.ToString());
            }

            return RedirectToAction(nameof(Index));
        }

        //Edit Akun


        //Ubah password


        //Ubah user name

    }
}
