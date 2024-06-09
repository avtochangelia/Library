using Library.App.Models.Configs;
using Library.App.Models.ViewModels.Account;
using Microsoft.Extensions.Options;
namespace Library.App.Services;

public class AccountService(HttpClient httpClient, IOptions<ApisConfig> apisConfig, IHttpContextAccessor httpContextAccessor)
    : AuthRestService(httpClient, apisConfig, httpContextAccessor)
{
    public async Task<UserViewModel?> GetUserAsync(Guid id)
    {
        var endpoint = $"{_apisConfig.LibraryApi}users/{id}";
        var result = await GetAsync<UserViewModel>(endpoint);
        return result;
    }

    public async Task<HttpResponseMessage> ChangePasswordAsync(ChangePasswordViewModel model)
    {
        var endpoint = $"{_apisConfig.LibraryApi}users/change-password";
        var result = await PatchAsync(endpoint, model);
        return result;
    }
}