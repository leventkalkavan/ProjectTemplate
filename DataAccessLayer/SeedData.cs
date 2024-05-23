using EntityLayer;
using EntityLayer.Identity;
using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        string adminRole = "Admin";
        string userRole = "User";

        foreach (var roleName in new[] { adminRole, userRole })
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                var roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        var adminUser = await userManager.FindByNameAsync("admin");
        if (adminUser == null)
        {
            var newAdminUser = new AppUser
            {
                UserName = "admin",
                Email = "admin@email.com",
                PhoneNumber = "11111111",
                Role = "Admin"
            };
            await userManager.CreateAsync(newAdminUser, "AdmininSifresiNeOlabilirki?1");
            await userManager.AddToRoleAsync(newAdminUser, adminRole);
        }
    }
}