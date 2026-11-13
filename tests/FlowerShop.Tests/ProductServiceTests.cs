using FlowerShop.Data;
using FlowerShop.Data.Models;
using FlowerShop.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FlowerShop.Tests;

public class ProductServiceTests
{
    private static FlowerShopDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<FlowerShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new FlowerShopDbContext(options);
    }

    private static async Task<(FlowerShopDbContext Db, Category Roses, Category Bouquets)> CreateSeededContextAsync()
    {
        var db = CreateContext();

        var roses = new Category { Name = "Рози" };
        var bouquets = new Category { Name = "Букети" };

        db.Products.AddRange(
            new Product { Name = "Червена роза", Description = "d", Price = 3m, StockQuantity = 5, Category = roses, CreatedOn = new DateTime(2027, 1, 1) },
            new Product { Name = "Бяла роза", Description = "d", Price = 3m, StockQuantity = 5, Category = roses, CreatedOn = new DateTime(2027, 1, 2) },
            new Product { Name = "Пролетен букет", Description = "d", Price = 20m, StockQuantity = 5, Category = bouquets, CreatedOn = new DateTime(2027, 1, 3) },
            new Product { Name = "Изчерпан букет", Description = "d", Price = 25m, StockQuantity = 0, IsAvailable = false, Category = bouquets, CreatedOn = new DateTime(2027, 1, 4) });

        await db.SaveChangesAsync();
        return (db, roses, bouquets);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOnlyAvailableProducts()
    {
        var (db, _, _) = await CreateSeededContextAsync();
        var service = new ProductService(db);

        var result = await service.GetAllAsync();

        Assert.Equal(3, result.Count);
        Assert.DoesNotContain(result, p => p.Name == "Изчерпан букет");
    }

    [Fact]
    public async Task GetAllAsync_FiltersByCategory()
    {
        var (db, roses, _) = await CreateSeededContextAsync();
        var service = new ProductService(db);

        var result = await service.GetAllAsync(categoryId: roses.Id);

        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.Equal("Рози", p.CategoryName));
    }

    [Fact]
    public async Task GetAllAsync_FiltersBySearchTerm()
    {
        var (db, _, _) = await CreateSeededContextAsync();
        var service = new ProductService(db);

        var result = await service.GetAllAsync(search: "Бяла");

        var product = Assert.Single(result);
        Assert.Equal("Бяла роза", product.Name);
    }

    [Fact]
    public async Task GetLatestAsync_ReturnsNewestFirstAndRespectsCount()
    {
        var (db, _, _) = await CreateSeededContextAsync();
        var service = new ProductService(db);

        var result = await service.GetLatestAsync(2);

        Assert.Equal(2, result.Count);
        Assert.Equal("Пролетен букет", result[0].Name);
        Assert.Equal("Бяла роза", result[1].Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenProductDoesNotExist()
    {
        var db = CreateContext();
        var service = new ProductService(db);

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProduct_WhenItExists()
    {
        var (db, _, _) = await CreateSeededContextAsync();
        var service = new ProductService(db);
        var id = (await db.Products.FirstAsync(p => p.Name == "Червена роза")).Id;

        var result = await service.GetByIdAsync(id);

        Assert.NotNull(result);
        Assert.Equal("Червена роза", result!.Name);
        Assert.Equal("Рози", result.CategoryName);
    }

    [Fact]
    public async Task GetCategoriesAsync_ReturnsCategoriesOrderedByName()
    {
        var (db, _, _) = await CreateSeededContextAsync();
        var service = new ProductService(db);

        var result = await service.GetCategoriesAsync();

        Assert.Equal(new[] { "Букети", "Рози" }, result.Select(c => c.Name).ToArray());
    }
}
