using FlowerShop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Data;

public class FlowerShopDbContext : IdentityDbContext<ApplicationUser>
{
    public FlowerShopDbContext(DbContextOptions<FlowerShopDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<CartItem> CartItems => Set<CartItem>();

    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>(e =>
        {
            e.HasIndex(c => c.Name).IsUnique();
        });

        builder.Entity<Product>(e =>
        {
            e.Property(p => p.Price).HasPrecision(18, 2);
            e.HasIndex(p => p.Name);

            e.HasOne(p => p.Category)
             .WithMany(c => c.Products)
             .HasForeignKey(p => p.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Order>(e =>
        {
            e.Property(o => o.TotalPrice).HasPrecision(18, 2);

            // Поръчките се пазят дори потребителят да бъде изтрит от админ - затова Restrict.
            e.HasOne(o => o.User)
             .WithMany(u => u.Orders)
             .HasForeignKey(o => o.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<OrderItem>(e =>
        {
            e.Property(oi => oi.UnitPrice).HasPrecision(18, 2);

            e.HasOne(oi => oi.Order)
             .WithMany(o => o.Items)
             .HasForeignKey(oi => oi.OrderId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(oi => oi.Product)
             .WithMany()
             .HasForeignKey(oi => oi.ProductId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<CartItem>(e =>
        {
            // Един продукт се среща най-много веднъж в количката на потребителя.
            e.HasIndex(ci => new { ci.UserId, ci.ProductId }).IsUnique();

            e.HasOne(ci => ci.User)
             .WithMany(u => u.CartItems)
             .HasForeignKey(ci => ci.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(ci => ci.Product)
             .WithMany()
             .HasForeignKey(ci => ci.ProductId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Review>(e =>
        {
            // Един потребител - един отзив за продукт.
            e.HasIndex(r => new { r.ProductId, r.UserId }).IsUnique();

            e.HasOne(r => r.Product)
             .WithMany(p => p.Reviews)
             .HasForeignKey(r => r.ProductId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(r => r.User)
             .WithMany(u => u.Reviews)
             .HasForeignKey(r => r.UserId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
