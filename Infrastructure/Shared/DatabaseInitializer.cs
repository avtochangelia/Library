#nullable disable

using Domain.UserManagement.Enums;
using Domain.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared;

public class EFDatabaseInitializer
{
    public static void Initialize(IServiceScope serviceScope)
    {
        _ = new EFDatabaseInitializer();
        Seed(serviceScope);
    }

    protected static void Seed(IServiceScope serviceScope)
    {
        var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
        var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
        var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();

        if (roleManager != null)
        {
            foreach (UserRole role in (UserRole[])Enum.GetValues(typeof(UserRole)))
            {
                var roleExist = roleManager.FindByNameAsync(role.ToString()).Result;

                if (roleExist == null)
                {
                    _ = roleManager.CreateAsync(new IdentityRole { Name = role.ToString() }).Result;
                }
            }
        }

        if (userManager != null)
        {
            var userExists = userManager.GetUsersInRoleAsync(UserRole.Admin.ToString()).Result;

            if (userExists.Count == 0 && configuration != null)
            {
                var adminCredentialsConfig = configuration.GetSection("AdminCredentialsConfig");
                var user = new User(adminCredentialsConfig["FirstName"],
                                    adminCredentialsConfig["LastName"],
                                    adminCredentialsConfig["UserName"],
                                    adminCredentialsConfig["Email"]);
                _ = userManager.CreateAsync(user, adminCredentialsConfig["Password"]).Result;
                _ = userManager.AddToRoleAsync(user, UserRole.Admin.ToString()).Result;
            }
        }
    }
}