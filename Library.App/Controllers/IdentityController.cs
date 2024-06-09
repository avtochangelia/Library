using Library.App.Helpers;
using Library.App.Models.ViewModels.Identity;
using Library.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.App.Controllers;

public class IdentityController(IdentityService identityService) : Controller
{
    private readonly IdentityService _identityService = identityService;

    [HttpPost]
    public async Task<IActionResult> Login(string userName, string password)
    {
        if (ModelState.IsValid)
        {
            var authResponse = await _identityService.AuthenticateAsync(userName, password);

            if (!authResponse.Success)
            {
                TempData["ErrorMessage"] = $"ავტორიზაცია ვერ მოხერხდა.";
                return RedirectToAction("Index", "Home");
            }

            HttpContext.Session.SetString("Token", authResponse.Token);
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelStateHelper.HandleModelStateErrors(this);
            return View("Register", model);
        }

        var registerResponse = await _identityService.RegisterAsync(model);

        if (registerResponse.IsSuccessStatusCode)
        {
            var authResponse = await _identityService.AuthenticateAsync(model.UserName, model.Password);

            if (authResponse.Success)
            {
                HttpContext.Session.SetString("Token", authResponse.Token);
                return RedirectToAction("Index", "Home");
            }
        }

        TempData["ErrorMessage"] = $"რეგისტრაცია ვერ მოხერხდა.";
        return View("Register", model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}