using HVACrate.Extensions;
using ElectronNET.API;
using ElectronNET.API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Builder Configurations for Electron
builder.Configuration
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddEnvironmentVariables();
builder.Configuration.AddUserSecrets<Program>(optional: true);

// Configure Electron
builder.WebHost.UseElectron(args);
builder.Services.AddElectron();

// Database concerns
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// App concerns
builder.Services.AddIdentity();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddControllersWithViews();

// Identity scaffolding
builder.Services.AddRazorPages();

var app = builder.Build();

// Seed identity data
await app.Services.SeedIdentityDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

await app.StartAsync();

var window = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
{
    Width = 1920,
    Height = 1080,
    Show = false
});

window.OnReadyToShow += () => window.Show();

await app.WaitForShutdownAsync();