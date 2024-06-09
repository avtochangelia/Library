using Shared.Constants;
using System.ComponentModel;

namespace Domain.UserManagement.Enums;
public enum UserRole
{
    [Description(UserRoleNames.Admin)]
    Admin = 1,

    [Description(UserRoleNames.User)]
    User = 2,
}