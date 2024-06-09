using Library.App.Helpers;
using Library.App.Models;
using Library.App.Models.ViewModels.Account;
using Library.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library.App.Controllers;

public class AccountController(AccountService accountService) : Controller
{
    private readonly AccountService _accountService = accountService;

    public async Task<IActionResult> Index()
    {
        var userId = Guid.Parse(User.FindFirst("userId")!.Value);
        var user = await _accountService.GetUserAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelStateHelper.HandleModelStateErrors(this);
            return View("ChangePassword", model);
        }

        var result = await _accountService.ChangePasswordAsync(model);

        if (result.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "პაროლი წარმატებით შეიცვალა.";
            return RedirectToAction(nameof(Index));
        }

        TempData["ErrorMessage"] = "პაროლი ვერ შეიცვალა.";
        return RedirectToAction(nameof(ChangePassword));
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}