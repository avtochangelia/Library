using Domain.Shared.Repositories;
using Domain.UserManagement.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.UserManagement.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IdentityResult> InsertAsync(User aggregateRoot, string password, UserRole role);

    Task<IdentityResult> UpdateAsync(User user);

    Task<IdentityResult> DeleteAsync(User user);

    Task<(bool success, User? user)> ValidateUserAsync(string userName, string password);

    Task<IList<string>> GetUserRolesAsync(User user);

    Task<string> GeneratePasswordResetTokenAsync(User user);

    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);

    Task<bool> CheckPasswordAsync(User user, string password);

    Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword);

    Task<IdentityResult> ChangeEmailAsync(User user, string email);

    Task<IdentityResult> ChangeUserNameAsync(User user, string userName);

    Task<User?> FindByEmailAsync(string email);

    Task<User?> FindByNameAsync(string userName);
}