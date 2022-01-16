using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataLayer;
using Moq;
using Option1ServiceLayer.Tests.ServiceTests.CustomerServiceTests;
using ServiceLayer.Models;
using ServiceLayer.Service.Orders;
using Shouldly;
using SimpleRepo.Repo;
using Xunit;

namespace Option1ServiceLayer.Tests.ServiceTests.OrderServiceTests;

public class GetOrdersForCustomerTests
{
    private const string Email = "test@gmail.com";
    private const int CustomerId = 5324;

    [Fact]
    public async Task Ensure_we_can_Get_customer_by_email()
    {
        var (sut, mockrepo) = OrderServiceFactory.Generate();
        await Ensure_we_can_GetCustomerByX(() => sut.GetOrdersForCustomer(Email), sut, mockrepo);
    }

    [Fact]
    public async Task Ensure_we_can_Get_customer_by_id()
    {
        var (sut, mockrepo) = OrderServiceFactory.Generate();
        await Ensure_we_can_GetCustomerByX(() => sut.GetOrdersForCustomer(CustomerId), sut, mockrepo);
    }

    public async Task Ensure_we_can_GetCustomerByX(Func<Task<OrderResponse>> func, OrderService sut,
        Mock<IRepo<ExampleDbContext>> mockrepo)
    {
        var goodCustomer = new Customer()
        {
            Id = CustomerId,
            Email = Email,
            Orders = new List<Order>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    CustomerId = CustomerId,
                    Total = 5263
                }
            }
        };

        var list = new List<Customer>()
        {
            goodCustomer,
            new()
            {
                Id = 512,
                Email = "g532w@gwe.com",
                Orders = new List<Order>
                {
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Created = DateTime.Now.AddDays(-1),
                        CustomerId = 512,
                        Total = 510
                    }
                }
            }
        };
        mockrepo.Setup(t =>
                t.Get(It.IsAny<Expression<Func<Customer, bool>>>(),
                    It.IsAny<Expression<Func<Customer, OrderResponse>>>()))
            .ReturnsAsync((Expression<Func<Customer, bool>> func,
                    Expression<Func<Customer, OrderResponse>> pro) =>
                list.Where(func.Compile()).Select(pro.Compile()).FirstOrDefault());

        var ordersForCustomer = await func.Invoke();

        ordersForCustomer.CustomerId.ShouldBe(goodCustomer.Id);
        ordersForCustomer.Orders.Count().ShouldBe(1);
        ordersForCustomer.Orders.First().Total.ShouldBe(goodCustomer.Orders.First().Total);
        ordersForCustomer.Orders.First().CreatedAt.ShouldBe(goodCustomer.Orders.First().Created);
        ordersForCustomer.Orders.First().OrderId.ShouldBe(goodCustomer.Orders.First().Id);
    }
}