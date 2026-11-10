using FlowerShop.Data;
using FlowerShop.Data.Models;
using FlowerShop.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Services;

public class ProductService : IProductService
{
    private readonly FlowerShopDbContext _db;

    public ProductService(FlowerShopDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(int? categoryId = null, string? search = null)
    {
        IQueryable<Product> query = _db.Products
            .AsNoTracking()
            .Where(p => p.IsAvailable);

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(p => p.Name.Contains(term));
        }

        return await query
            .OrderBy(p => p.Name)
            .Select(ToDto)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductDto>> GetLatestAsync(int count)
    {
        return await _db.Products
            .AsNoTracking()
            .Where(p => p.IsAvailable)
            .OrderByDescending(p => p.CreatedOn)
            .ThenBy(p => p.Name)
            .Take(count)
            .Select(ToDto)
            .ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        return await _db.Products
            .AsNoTracking()
            .Where(p => p.Id == id && p.IsAvailable)
            .Select(ToDto)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesAsync()
    {
        return await _db.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
            .ToListAsync();
    }

    public Task<int> CountAsync() => _db.Products.CountAsync();

    private static readonly System.Linq.Expressions.Expression<Func<Product, ProductDto>> ToDto =
        p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            StockQuantity = p.StockQuantity,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name
        };
}
