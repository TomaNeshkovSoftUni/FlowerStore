using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FlowerShop.Data.Models;

/// <summary>Потребител на платформата (разширява ASP.NET Core Identity).</summary>
public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    public DateTime RegisteredOn { get; set; } = DateTime.UtcNow;

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
