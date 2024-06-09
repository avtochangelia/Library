using Library.App.Models.Configs;
using Library.App.Models.ViewModels.Books;
using Microsoft.Extensions.Options;

namespace Library.App.Services;

public class BookService(HttpClient httpClient, IOptions<ApisConfig> apisConfig, IHttpContextAccessor httpContextAccessor)
    : AuthRestService(httpClient, apisConfig, httpContextAccessor)
{
    public async Task<BookViewModel?> GetBookByIdAsync(Guid id)
    {
        var endpoint = $"{_apisConfig.LibraryApi}books/{id}";
        var result = await GetAsync<BookViewModel>(endpoint);
        return result;
    }

    public async Task<BooksPagedResult?> GetBooksAsync(int pageIndex, int pageSize, string? searchQuery = null)
    {
        var endpoint = $"{_apisConfig.LibraryApi}books?page={pageIndex}&pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            endpoint += $"&searchQuery={searchQuery}";
        }

        return await GetAsync<BooksPagedResult>(endpoint);
    }

    public async Task<HttpResponseMessage> CreateBookAsync(CreateBookModel model)
    {
        var endpoint = $"{_apisConfig.LibraryApi}books";
        return await PostAsync(endpoint, model);
    }

    public async Task<HttpResponseMessage> UpdateBookAsync(string id, UpdateBookModel model)
    {
        var endpoint = $"{_apisConfig.LibraryApi}books/{id}";
        return await PutAsync(endpoint, model);
    }

    public async Task<HttpResponseMessage> DeleteBookAsync(string id)
    {
        var endpoint = $"{_apisConfig.LibraryApi}books/{id}";
        return await DeleteAsync(endpoint);
    }

    public async Task<HttpResponseMessage> BringInBookAsync(string id)
    {
        var endpoint = $"{_apisConfig.LibraryApi}books/{id}/bring-in";
        return await PatchAsync(endpoint, new BookPatchModel());
    }

    public async Task<HttpResponseMessage> TakeOutBookAsync(string id)
    {
        var endpoint = $"{_apisConfig.LibraryApi}books/{id}/take-out";
        return await PatchAsync(endpoint, new BookPatchModel());
    }
}