using BrioBook.Account.Models.Views;
using BrioBook.Account.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrioBook.Account.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IManagerAccounts _managerAccounts;

        public AccountController(IManagerAccounts managerAccounts)
        {
            _managerAccounts = managerAccounts;
        }

        public IActionResult Index(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] AccountViewModel data, string returnUrl)
        {
            var result = await _managerAccounts.SingIn(data.Login, data.Password);

            if (result.Succeeded)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, data.Login)
                    };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, result.Errors);

            return View(data);
        }

        public IActionResult Create(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountViewModel data, [FromRoute] string returnUrl)
        {

            var result = await _managerAccounts.SingUp(data.Login, data.Password);

            if (result.Succeeded)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, data.Login)
                    };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, result.Errors);

            return View(data);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;

            return View();
        }
    }
}
