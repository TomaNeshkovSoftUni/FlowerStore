namespace FlowerShop.Web.Models;

public class OrderListItemViewModel
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime DeliveryDate { get; set; }

    public string Status { get; set; } = null!;

    public decimal TotalPrice { get; set; }
}
