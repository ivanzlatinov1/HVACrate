using HVACrate.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Database concerns
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// App concerns
builder.Services.AddIdentity();
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
