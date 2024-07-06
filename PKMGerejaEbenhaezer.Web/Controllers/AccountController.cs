using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKMGerejaEbenhaezer.DataAccess.Data;
using PKMGerejaEbenhaezer.Domain.Entity;
using PKMGerejaEbenhaezer.Web.Authentication;
using PKMGerejaEbenhaezer.Web.Models.Account;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ISignInManager _signInManager;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ISignInManager signInManager, 
            AppDbContext appDbContext, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home", new { Area = "Dashboard" });
            ViewData["returnUrl"] = returnUrl;

            return View(new LoginVM());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM, string? returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home", new { Area = "Dashboard" });
            ViewData["returnUrl"] = returnUrl;

            if (ModelState.IsValid == false) return View(loginVM);

            var result = await _signInManager.SignInAsync(loginVM.UserName, loginVM.Password, loginVM.RememberMe);

            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Login Gagal!. User name atau password salah.");
                return View(loginVM);
            }

            return Redirect(returnUrl!);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var result = await _signInManager.SignOut();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [AllowAnonymous]
        public IActionResult AccessDenied(string? returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        //Ubah password
        public IActionResult UbahPassword()
        {
            return View(new UbahPasswordVM());
        }

        [HttpPost]
        public async Task<IActionResult> UbahPassword(UbahPasswordVM ubahPasswordVM)
        {
            //Validasi
            if (!ModelState.IsValid)
                return View(ubahPasswordVM);

            var userName = User.Identity?.Name;
            var user = await _appDbContext.AppUserTable
                .Where(p => p.UserName == userName).FirstOrDefaultAsync();

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Anda harus login terlebih dahulu untuk merubah password");
                return View(ubahPasswordVM);
            }

            var hasher = new PasswordHasher<AppUser>();

            var verificationResult = hasher.VerifyHashedPassword(null,
                user.PasswordHash, ubahPasswordVM.Password);

            if (verificationResult == PasswordVerificationResult.Success ||
                verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                ModelState.AddModelError(nameof(UbahPasswordVM.Password), "Password baru sama dengan password lama");
                return View(ubahPasswordVM);
            }

            //Simpan ke database
            user.PasswordHash = hasher.HashPassword(null, ubahPasswordVM.Password);

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error saat menyimpan data. Silahkan laporkan ke administrator");
                _logger.LogError(
                """
                    Ubah Password Gagal. 
                    User = {0}.
                    Exception : {1}
                """, userName, ex.ToString());
                return View(ubahPasswordVM);
            }

            return RedirectToAction(nameof(Login));
        }

        //Ubah user name
        public IActionResult UbahUserName()
        {
            return View(new UbahUserNameVM());
        }

        [HttpPost]
        public async Task<IActionResult> UbahUserName(UbahUserNameVM ubahUserNameVM)
        {
            //Validasi
            if (!ModelState.IsValid) return View(ubahUserNameVM);

            var userName = User.Identity?.Name;

            var user = await _appDbContext.AppUserTable
                .Where(u => u.UserName == userName).FirstOrDefaultAsync();

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Anda harus login terlebih dahulu sebelum merubah user name");
                return View(ubahUserNameVM);
            }

            var duplikasiNama = await _appDbContext.AppUserTable
                .AnyAsync(u => u.Id != user.Id && u.UserName == userName);

            if (duplikasiNama)
            {
                ModelState.AddModelError(nameof(UbahUserNameVM.UserName),
                    $"{ubahUserNameVM.UserName} sudah digunakan!. Gunakan nama lain.");
                return View(ubahUserNameVM);
            }

            user.UserName = ubahUserNameVM.UserName;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Simpan gagal, terjadi error saat menyimpan ke database. Silahkan hubungi administrator");
                _logger.LogError("UbahUserName. Error simpan ke database. UserName : {0}. Exception : {1}", userName, ex.ToString());
                return View(ubahUserNameVM);
            }

            return RedirectToAction(nameof(Login));
        }
    }
}
