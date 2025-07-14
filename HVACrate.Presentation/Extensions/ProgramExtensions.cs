using HVACrate.Application.Interfaces;
using HVACrate.Application.Services;
using HVACrate.Domain.Entities;
using HVACrate.Domain.Repositories.BuildingEnvelopes;
using HVACrate.Domain.Repositories.Buildings;
using HVACrate.Domain.Repositories.Projects;
using HVACrate.Domain.Repositories.Rooms;
using HVACrate.Domain.Repositories.Users;
using HVACrate.Persistence;
using HVACrate.Persistence.Repositories.BuildingEnvelopes;
using HVACrate.Persistence.Repositories.Buildings;
using HVACrate.Persistence.Repositories.Projects;
using HVACrate.Persistence.Repositories.Rooms;
using HVACrate.Persistence.Repositories.Users;
using HVACrate.Persistence.Seeding;
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

            options.User.RequireUniqueEmail = true;

            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
        })
            .AddEntityFrameworkStores<HVACrateDbContext>()
            .AddDefaultTokenProviders();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, HVACUserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IBuildingRepository, BuildingRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IBuildingEnvelopeRepository, BuildingEnvelopeRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IBuildingService, BuildingService>();
        services.AddScoped<IRoomService, RoomService>();
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