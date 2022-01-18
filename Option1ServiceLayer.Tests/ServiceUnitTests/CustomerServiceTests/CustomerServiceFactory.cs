using DataLayer;
using Moq;
using ServiceLayer.Service.Customer;
using SimpleRepo.Repo;

namespace Option1ServiceLayer.Tests.ServiceUnitTests.CustomerServiceTests;

public class CustomerServiceFactory
{
    public static (CustomerService, Mock<IRepo<ExampleDbContext>>) Generate()
    {
        var mock = new Mock<IRepo<ExampleDbContext>>();
        return (new CustomerService(mock.Object), mock);
    }
}