using Library.App.Models.Configs;
using Library.App.Models.ViewModels.Authors;
using Microsoft.Extensions.Options;

namespace Library.App.Services;

public class AuthorService(HttpClient httpClient, IOptions<ApisConfig> apisConfig, IHttpContextAccessor httpContextAccessor) 
    : AuthRestService(httpClient, apisConfig, httpContextAccessor)
{
    public async Task<AuthorsPagedResult?> GetAuthorsAsync(int pageIndex, int pageSize, string? searchQuery = null)
    {
        var endpoint = $"{_apisConfig.LibraryApi}authors?page={pageIndex}&pageSize={pageSize}";

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            endpoint += $"&searchQuery={searchQuery}";
        }

        return await GetAsync<AuthorsPagedResult>(endpoint);
    }

    public async Task<HttpResponseMessage> CreateAuthorAsync(CreateAuthorModel model)
    {
        var endpoint = $"{_apisConfig.LibraryApi}authors";
        return await PostAsync(endpoint, model);
    }

    public async Task<HttpResponseMessage> UpdateAuthorAsync(string id, UpdateAuthorModel model)
    {
        var endpoint = $"{_apisConfig.LibraryApi}authors/{id}";
        return await PutAsync(endpoint, model);
    }

    public async Task<HttpResponseMessage> DeleteAuthorAsync(string id)
    {
        var endpoint = $"{_apisConfig.LibraryApi}authors/{id}";
        return await DeleteAsync(endpoint);
    }
}