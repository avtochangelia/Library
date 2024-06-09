#nullable disable

using System.Text.Json.Serialization;

namespace Library.App.Models.ResponseModels;

public class AuthResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("token")]
    public string Token { get; set; }
}