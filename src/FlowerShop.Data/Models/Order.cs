using System.ComponentModel.DataAnnotations;

namespace FlowerShop.Data.Models;

/// <summary>Поръчка на клиент.</summary>
public class Order
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    [Required]
    [MaxLength(300)]
    public string DeliveryAddress { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = null!;

    public DateTime DeliveryDate { get; set; }

    /// <summary>Послание, което да бъде приложено към букета.</summary>
    [MaxLength(300)]
    public string? CardMessage { get; set; }

    public decimal TotalPrice { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
