namespace ServiceLayer.Service.Orders;

public class OrderDetails
{
    public Guid OrderId { get; set; }
    public DateTime CreatedAt{get; set; }
    public decimal Total { get; set; }
}