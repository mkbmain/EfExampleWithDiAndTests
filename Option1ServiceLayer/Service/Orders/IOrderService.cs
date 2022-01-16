using ServiceLayer.Models;

namespace ServiceLayer.Service.Orders;

public interface IOrderService : IBaseService<DataLayer.Order>
{
    public Task<Guid> AddOrder(CreateOrderRequest request);

    public Task<OrderResponse> GetOrdersForCustomer(int customerId);
    
    public Task<OrderResponse> GetOrdersForCustomer(string customerEmail);
}