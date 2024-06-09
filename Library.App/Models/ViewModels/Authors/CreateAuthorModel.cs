#nullable disable

namespace Library.App.Models.ViewModels.Authors;

public class CreateAuthorModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}