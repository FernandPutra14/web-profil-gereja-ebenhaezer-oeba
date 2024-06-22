using Microsoft.AspNetCore.Mvc;
using PKMGerejaEbenhaezer.Web.Authentication;
using PKMGerejaEbenhaezer.Web.Models.Account;

namespace PKMGerejaEbenhaezer.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISignInManager _signInManager;

        public AccountController(ISignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid == false) return View(loginVM);

            var result = await _signInManager.SignInAsync(loginVM.UserName, loginVM.Password, loginVM.RememberMe);

            if (result == false)
            {
                ModelState.AddModelError(string.Empty, "Login Gagal!. User name atau password salah.");
                return View(loginVM);
            }

            return RedirectToAction("Index", "Home", new { Area = "Dashboard" });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            var result = await _signInManager.SignOut();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }
    }
}
