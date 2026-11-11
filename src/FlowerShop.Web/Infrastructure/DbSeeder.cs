using FlowerShop.Data;
using FlowerShop.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Web.Infrastructure;

/// <summary>
/// Прилага миграциите и създава начални данни: роли, администратор и примерен каталог.
/// Данните за администратора се четат от конфигурацията (User Secrets) - никога не са в кода.
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration)
    {
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("DbSeeder");
        var db = services.GetRequiredService<FlowerShopDbContext>();

        await db.Database.MigrateAsync();

        await SeedRolesAsync(services);
        await SeedAdminAsync(services, configuration, logger);
        await SeedCatalogAsync(db);
    }

    private static async Task SeedRolesAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in new[] { RoleNames.Admin, RoleNames.Customer })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedAdminAsync(IServiceProvider services, IConfiguration configuration, ILogger logger)
    {
        var email = configuration["Admin:Email"];
        var password = configuration["Admin:Password"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            logger.LogWarning("Не са зададени Admin:Email и Admin:Password в User Secrets - администратор няма да бъде създаден.");
            return;
        }

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var admin = await userManager.FindByEmailAsync(email);

        if (admin is null)
        {
            admin = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FirstName = "Администратор",
                LastName = "Цветарница"
            };

            var result = await userManager.CreateAsync(admin, password);
            if (!result.Succeeded)
            {
                logger.LogError("Администраторът не може да бъде създаден: {Errors}",
                    string.Join("; ", result.Errors.Select(e => e.Description)));
                return;
            }
        }

        if (!await userManager.IsInRoleAsync(admin, RoleNames.Admin))
        {
            await userManager.AddToRoleAsync(admin, RoleNames.Admin);
        }
    }

    private static async Task SeedCatalogAsync(FlowerShopDbContext db)
    {
        if (await db.Categories.AnyAsync())
        {
            return;
        }

        var bouquets = new Category { Name = "Букети", Description = "Готови букети за всеки повод." };
        var roses = new Category { Name = "Рози", Description = "Рози в различни цветове и дължини." };
        var potted = new Category { Name = "Саксийни цветя", Description = "Цветя в саксия за дома и офиса." };
        var gifts = new Category { Name = "Подаръчни комплекти", Description = "Цветя в комбинация с подарък." };

        db.Categories.AddRange(bouquets, roses, potted, gifts);

        db.Products.AddRange(
            new Product { Name = "Пролетен микс", Description = "Свежа композиция от лалета, фрезии и зеленина.", Price = 24.90m, StockQuantity = 15, Category = bouquets },
            new Product { Name = "Нежност", Description = "Букет от пудрени рози и еустома.", Price = 32.50m, StockQuantity = 10, Category = bouquets },
            new Product { Name = "Слънчоглед и маргарита", Description = "Ярък букет с жълти и бели цветя.", Price = 21.00m, StockQuantity = 12, Category = bouquets },
            new Product { Name = "Червени рози (11 броя)", Description = "Класически букет от 11 червени рози, 60 см.", Price = 29.90m, StockQuantity = 20, Category = roses },
            new Product { Name = "Бели рози (7 броя)", Description = "Букет от 7 бели рози с гипсофила.", Price = 22.00m, StockQuantity = 18, Category = roses },
            new Product { Name = "Орхидея Фаленопсис", Description = "Бяла орхидея в керамична кашпа.", Price = 27.00m, StockQuantity = 8, Category = potted },
            new Product { Name = "Мини кактуси (комплект 3 бр.)", Description = "Три малки кактуса в декоративни саксии.", Price = 14.50m, StockQuantity = 25, Category = potted },
            new Product { Name = "Букет и шоколадови бонбони", Description = "Малък букет и кутия бонбони.", Price = 34.90m, StockQuantity = 7, Category = gifts });

        await db.SaveChangesAsync();
    }
}
