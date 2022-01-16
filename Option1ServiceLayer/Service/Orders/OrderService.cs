using System.Linq.Expressions;
using DataLayer;
using ServiceLayer.Models;
using SimpleRepo.Repo;

namespace ServiceLayer.Service.Orders;

public class OrderService : BaseService<DataLayer.Order>, IOrderService
{
    public OrderService(IRepo<ExampleDbContext> repo) : base(repo)
    {
    }

    public async Task<Guid> AddOrder(CreateOrderRequest request)
    {
        var customer =
            await Repo.Get<DataLayer.Customer, DataLayer.Customer>(t => t.Email == request.CustomerEmail, t => t);
        if (customer == null)
        {
            throw new ArgumentException("No customer found");
        }

        var order = new Order
        {
            CustomerId = customer.Id,
            Created = DateTime.Now,
            Total = request.Total
        };
        await Repo.Add(order);
        return order.Id;
    }

    public Task<OrderResponse> GetOrdersForCustomer(int customerId)
    {
        return GetOrdersForCustomer(t => t.Id == customerId);
    }

    public Task<OrderResponse> GetOrdersForCustomer(string customerEmail)
    {
        return GetOrdersForCustomer(t => t.Email == customerEmail);
    }

    protected Task<OrderResponse> GetOrdersForCustomer(Expression<Func<DataLayer.Customer, bool>> func)
    {
        return Repo.Get(func, t => new OrderResponse
        {
            CustomerId = t.Id,
            Orders = t.Orders.Select(order => new OrderDetails
            {
                CreatedAt = order.Created,
                OrderId = order.Id,
                Total = order.Total
            }).ToList()
        });
    }
}