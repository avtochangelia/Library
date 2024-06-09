#nullable disable

using Library.App.Models.Configs;
using Library.App.Models.ResponseModels;
using Library.App.Models.ViewModels.Identity;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Library.App.Services;

public class IdentityService(HttpClient httpClient, IOptions<ApisConfig> apiConfig)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly ApisConfig _apiConfig = apiConfig.Value;

    public async Task<AuthResponse> AuthenticateAsync(string username, string password)
    {
        var authEndpoint = _apiConfig.IdentityApi + "auth";
        var credentials = new { username, password };

        var content = new StringContent(JsonSerializer.Serialize(credentials), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(authEndpoint, content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent);

        return authResponse;
    }

    public async Task<HttpResponseMessage> RegisterAsync(RegisterViewModel model)
    {
        var registerEndpoint = _apiConfig.IdentityApi + "account/register";

        var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(registerEndpoint, content);

        return response;
    }
}