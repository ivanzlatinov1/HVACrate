using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HVACrate.Persistence.Seeding
{
    public static class IdentityData
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider, IConfiguration config, ILogger logger)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<HVACUser>>();

            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager, config, logger);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = ["Admin", "User"];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var identityRole = new IdentityRole<Guid> { Name = role, NormalizedName = role.ToUpper() };
                    var result = await roleManager.CreateAsync(identityRole);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }
        }

        private static async Task SeedAdminUserAsync(UserManager<HVACUser> userManager, IConfiguration config, ILogger logger)
        {
            string adminEmail = config["AdminUser:Email"] ?? throw new Exception("Admin email is missing from the config.");
            string adminPassword = config["AdminUser:Password"] ?? throw new Exception("Admin password is missing from the config.");

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new HVACUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(newAdmin, adminPassword);
                if (!createResult.Succeeded)
                {
                    logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    throw new Exception("Failed to create admin user.");
                }

                var addToRoleResult = await userManager.AddToRoleAsync(newAdmin, "Admin");
                if (!addToRoleResult.Succeeded)
                {
                    logger.LogError("Failed to assign Admin role: {Errors}", string.Join(", ", addToRoleResult.Errors.Select(e => e.Description)));
                    throw new Exception("Failed to assign Admin role.");
                }

                logger.LogInformation("Seeded admin user: {Email}", adminEmail);
            }
            else
            {
                logger.LogInformation("Admin user already exists: {Email}", adminEmail);
            }
        }
    }
}
