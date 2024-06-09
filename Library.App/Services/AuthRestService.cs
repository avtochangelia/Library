using Library.App.Models.Configs;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Library.App.Services;

public abstract class AuthRestService
{
    protected HttpClient _httpClient;
    protected readonly ApisConfig _apisConfig;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public AuthRestService(HttpClient httpClient, IOptions<ApisConfig> apisConfig, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _apisConfig = apisConfig.Value;

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var token = httpContextAccessor.HttpContext?.Items["Token"]?.ToString();

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    protected async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return Deserialize<T>(json);
    }

    protected async Task<HttpResponseMessage> PostAsync(string endpoint, object data)
    {
        var content = GenerateContent(data);
        return await _httpClient.PostAsync(endpoint, content);
    }

    protected async Task<HttpResponseMessage> PutAsync(string endpoint, object data)
    {
        var content = GenerateContent(data);
        return await _httpClient.PutAsync(endpoint, content);
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
    {
        return await _httpClient.DeleteAsync(endpoint);
    }

    protected virtual async Task<HttpResponseMessage> PatchAsync(string endpoint, object data)
    {
        var content = GenerateContent(data);
        return await _httpClient.PatchAsync(endpoint, content); 
    }

    private T? Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _jsonOptions);
    }

    private static StringContent GenerateContent(object data)
    {
        return new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
    }
}