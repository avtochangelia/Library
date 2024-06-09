using Library.App.Helpers;
using Library.App.Models;
using Library.App.Models.ViewModels.Authors;
using Library.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library.App.Controllers;

public class AuthorController(AuthorService authorService) : Controller
{
    private readonly AuthorService _authorService = authorService;

    public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10, string? searchQuery = null)
    {
        var authors = await _authorService.GetAuthorsAsync(pageIndex, pageSize, searchQuery);

        if (authors != null && searchQuery != null)
        {
            authors.SearchQuery = searchQuery;
        }

        return View(authors);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelStateHelper.HandleModelStateErrors(this);
            return RedirectToAction(nameof(Index));
        }

        var result = await _authorService.CreateAuthorAsync(model);

        if (!result.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = $"ავტორის დამატება ვერ მოხერხდა.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"ავტორი დაემატა.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, UpdateAuthorModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelStateHelper.HandleModelStateErrors(this);
            return RedirectToAction(nameof(Index));
        }

        var result = await _authorService.UpdateAuthorAsync(id, model);

        if (!result.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = $"ავტორის რედაქტირება ვერ მოხერხდა.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"ავტორი განახლდა.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _authorService.DeleteAuthorAsync(id);

        if (!result.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = $"ავტორის წაშლა ვერ მოხერხდა.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"ავტორი წაიშალა.";
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}