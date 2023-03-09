using BrioBook.Client.Models.Views;
using BrioBook.Client.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BrioBook.Client.Controllers;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IAuthenticationServiceClient _authenticateServiceClient;
    private readonly IConfirmClient _confirmClient;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        IAuthenticationServiceClient authenticateServiceClient,
        IConfirmClient confirmClient,
        ILogger<AccountController> logger)
    {
        _authenticateServiceClient = authenticateServiceClient;
        _confirmClient = confirmClient;
        _logger = logger;
    }

    public IActionResult Index(string returnUrl)
    {
        ViewData["returnUrl"] = returnUrl;

        _logger.LogInformation("Headers:\n" + string.Join('\n', Request.Headers));
        
        return View();
    }
    private IList<Claim> GetClaims(Dictionary<string, string> pairs)
    {
        List<Claim> claims = new List<Claim>();

        foreach (var pair in pairs)
        {
            claims.Add(new Claim(pair.Key, pair.Value));
        }

        return claims;
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] AccountViewModel data, string returnUrl)
    {
        var result = _authenticateServiceClient.Login(data.Login, data.Password);

        _logger.LogInformation($"{data.Login} - autentication is {result.Succeeded}"  + 
           (result.Succeeded ? "" : $"\nErrors: {result.Errors}"));

        if (result.Succeeded & result.AuthenticationUserData is not null)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(GetClaims(result.AuthenticationUserData.Claims), result.AuthenticationUserData.AuthenticationScheme);

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(result.AuthenticationUserData.AuthenticationScheme, claimsPrincipal);

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

        _logger.LogInformation("Headers:\n" + string.Join('\n', Request.Headers));

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AccountViewModel data, [FromRoute] string returnUrl)
    {

        var result = _authenticateServiceClient.Registration(data.Login, data.Password);

        _logger.LogInformation($"{data.Login} - autentication is {result.Succeeded}" +
           (result.Succeeded ? "" : $"\nErrors: {result.Errors}"));

        if (result.Succeeded)
        {
            return View("Seccesed", result);
        }

        ModelState.AddModelError(string.Empty, result.Errors);

        return View(data);
    }

    public IActionResult Confirm(string confirmId)
    {
        _logger.LogInformation("Headers:\n" + string.Join('\n', Request.Headers));

        if (Guid.TryParse(confirmId, out Guid result))
        {
            var response = _confirmClient.SetConfirm(result);

            ViewData["StatusConfirm"] = response.Succeeded;
            ViewData["ErrorsConfirm"] = response.Errors;
        }
        else
        {
            ViewData["StatusConfirm"] = false;
        }

        return View();
    }

    public async Task<IActionResult> LogOut()
    {
        _logger.LogInformation("Headers:\n" + string.Join('\n', Request.Headers));

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied(string returnUrl)
    {
        ViewData["returnUrl"] = returnUrl;

        _logger.LogInformation("Headers:\n" + string.Join('\n', Request.Headers));

        return View();
    }
}
