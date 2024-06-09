using Domain.Shared;
using Domain.UserManagement.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.UserManagement;

public class User : IdentityUser, IBaseEntity<string>
{
    public User()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public User(string firstName, string lastName, string userName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        Status = UserStatus.Active;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public UserStatus Status { get; private set; }

    public void ChangeDetails(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void ActivateUser()
    {
        Status = UserStatus.Active;
    }

    public void DeactivateUser()
    {
        Status = UserStatus.Inactive;
    }
}