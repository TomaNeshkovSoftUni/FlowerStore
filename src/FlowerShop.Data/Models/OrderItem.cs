namespace FlowerShop.Data.Models;

/// <summary>Ред от поръчка - продукт, количество и цена към момента на поръчката.</summary>
public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; } = null!;

    public int ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }

    /// <summary>Единична цена към момента на поръчката (пази се, за да не се променя историята).</summary>
    public decimal UnitPrice { get; set; }
}
