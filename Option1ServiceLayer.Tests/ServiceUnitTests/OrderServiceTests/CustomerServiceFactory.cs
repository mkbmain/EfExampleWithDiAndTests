using DataLayer;
using Moq;
using ServiceLayer.Service.Orders;
using SimpleRepo.Repo;

namespace Option1ServiceLayer.Tests.ServiceUnitTests.OrderServiceTests;

public class OrderServiceFactory
{
    public static (OrderService, Mock<IRepo<ExampleDbContext>>) Generate()
    {
        var mock = new Mock<IRepo<ExampleDbContext>>();
        return (new OrderService(mock.Object), mock);
    }
}