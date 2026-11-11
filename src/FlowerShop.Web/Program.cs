using FlowerShop.Data;
using FlowerShop.Data.Models;
using FlowerShop.Services;
using FlowerShop.Web.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- База данни (MS SQL Server) ----------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Липсва connection string 'DefaultConnection'.");

builder.Services.AddDbContext<FlowerShopDbContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly("FlowerShop.Data")));

// ---------- Автентикация и роли (ASP.NET Core Identity) ----------
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FlowerShopDbContext>();

// ---------- Сесия и бисквитки ----------
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = ".FlowerShop.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ---------- MVC ----------
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // нужно за Identity UI (вход, регистрация)

// ---------- Услуги на приложението (Dependency Injection) ----------
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// ---------- Миграции и начални данни ----------
using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedAsync(scope.ServiceProvider, app.Configuration);
}

// ---------- HTTP pipeline ----------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error/500");
    app.UseHsts();
}

// Собствени страници за грешки (404, 500 и др.)
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
