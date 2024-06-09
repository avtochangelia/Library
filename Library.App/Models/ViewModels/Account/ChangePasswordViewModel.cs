#nullable disable

using System.Text.Json.Serialization;

namespace Library.App.Models.ViewModels.Account;

public class ChangePasswordViewModel
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }

    [JsonIgnore]
    public string ConfirmNewPassword { get; set; }
}