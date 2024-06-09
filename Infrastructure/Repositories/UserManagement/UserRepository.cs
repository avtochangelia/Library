using Domain.UserManagement.Enums;
using Domain.UserManagement.Repositories;
using Domain.UserManagement;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Shared.Extensions;

namespace Infrastructure.Repositories.UserManagement;

public class UserRepository(EFDbContext context, UserManager<User> userManager)
    : EFBaseRepository<EFDbContext, User>(context), IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;

    public override async Task<User?> OfIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<IdentityResult> InsertAsync(User aggregateRoot, string password, UserRole role)
    {
        var result = await _userManager.CreateAsync(aggregateRoot, password);

        await _userManager.AddToRoleAsync(aggregateRoot, role.GetDescription());

        return result;
    }

    public async Task<IdentityResult> UpdateAsync(User user)
    {
        var result = await _userManager.UpdateAsync(user);

        return result;
    }

    public override void Update(User aggregateRoot)
    {
        _ = UpdateAsync(aggregateRoot).Result;
    }

    public async Task<IdentityResult> DeleteAsync(User user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result;
    }

    public async Task<(bool success, User? user)> ValidateUserAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user != null)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);

            if (user.Status != UserStatus.Active)
            {
                return (false, null);
            }

            return (result, user);
        }

        return (false, null);
    }

    public async Task<IList<string>> GetUserRolesAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<IdentityResult> ChangeEmailAsync(User user, string email)
    {
        return await _userManager.SetEmailAsync(user, email);
    }

    public async Task<IdentityResult> ChangeUserNameAsync(User user, string userName)
    {
        return await _userManager.SetUserNameAsync(user, userName);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }
}