using Library.App.Helpers;
using Library.App.Models;
using Library.App.Models.ViewModels.Books;
using Library.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library.App.Controllers;

public class BookController(BookService bookService, AuthorService authorService) : Controller
{
    private readonly BookService _bookService = bookService;
    private readonly AuthorService _authorService = authorService;

    public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 10, string? searchQuery = null)
    {
        var allAuthors = await _authorService.GetAuthorsAsync(1, int.MaxValue);
        var books = await _bookService.GetBooksAsync(pageIndex, pageSize, searchQuery);

        if (books != null && searchQuery != null)
        {
            books.SearchQuery = searchQuery;
        }

        if (allAuthors != null)
        {
            books!.AllAuthors = allAuthors.Authors;
        }

        return View(books);
    }

    public async Task<IActionResult> GetBook(Guid id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelStateHelper.HandleModelStateErrors(this);
            return RedirectToAction(nameof(Index));
        }

        var result = await _bookService.CreateBookAsync(model);

        if (!result.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = $"წიგნის დამატება ვერ მოხერხდა.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"წიგნი დაემატა.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, UpdateBookModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelStateHelper.HandleModelStateErrors(this);
            return RedirectToAction(nameof(Index));
        }

        var result = await _bookService.UpdateBookAsync(id, model);

        if (!result.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = $"წიგნის რედაქტირება ვერ მოხერხდა.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"წიგნი განახლდა.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _bookService.DeleteBookAsync(id);

        if (!result.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "წიგნის წაშლა ვერ მოხერხდა.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"წიგნი წაიშალა.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> BringIn(string id)
    {
        var result = await _bookService.BringInBookAsync(id);

        if (!result.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "წიგნი უკვე შემოტანილია.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"თქვენ წარმატებით შემოიტანეთ წიგნი.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> TakeOut(string id)
    {
        var result = await _bookService.TakeOutBookAsync(id);

        if (!result.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = "წიგნი უკვე გატანილია.";
            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = $"თქვენ წარმატებით გაიტანეთ წიგნი.";
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}