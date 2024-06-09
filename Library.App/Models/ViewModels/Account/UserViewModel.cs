#nullable disable

using System.Text.Json.Serialization;

namespace Library.App.Models.ViewModels.Account;

public class UserViewModel
{
    public UserDetail User { get; set; }

    public class UserDetail
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}