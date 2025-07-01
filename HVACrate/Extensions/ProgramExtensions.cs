using HVACrate.Data;
using HVACrate.Data.Models;
using HVACrate.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

#pragma warning disable IDE0130
namespace HVACrate.Extensions;
#pragma warning restore IDE0130

public static class ProgramExtensions
{
    public static void AddApplicationDbContext(this IServiceCollection services, IConfiguration configs)
    {
        var connectionString = configs.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<HVACrateDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.UseLazyLoadingProxies();
            });
    }

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<HVACUser, IdentityRole<Guid>>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;

            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
        })
            .AddEntityFrameworkStores<HVACrateDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddServices(this IServiceCollection services)
    {
        // TODO: Implement services and attach them to the container
    }

    public static async Task SeedIdentityDataAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedServices = scope.ServiceProvider;

        var config = scopedServices.GetRequiredService<IConfiguration>();
        var logger = scopedServices.GetRequiredService<ILoggerFactory>().CreateLogger("Seeding");

        await IdentityData.SeedDataAsync(scopedServices, config, logger);
    }
}