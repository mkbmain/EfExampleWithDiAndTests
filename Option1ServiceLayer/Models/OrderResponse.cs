namespace ServiceLayer.Service.Orders;

public class OrderResponse
{
    public int CustomerId { get; set; }
    public IEnumerable<OrderDetails> Orders { get; set; }
}