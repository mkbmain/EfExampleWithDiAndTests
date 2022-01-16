using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataLayer;
using Moq;
using ServiceLayer.Models;
using Shouldly;
using Xunit;

namespace Option1ServiceLayer.Tests.ServiceTests.OrderServiceTests;

public class AddOrderTests
{
    [Fact]
    public async Task Ensure_if_Customer_is_not_found_we_throw()
    {
        var (sut, mockrepo) = OrderServiceFactory.Generate();
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await sut.AddOrder(new CreateOrderRequest {CustomerEmail = "test@test.com"}));
        exception.Message.ShouldContain("No customer found");
    }

    [Fact]
    public async Task Ensure_we_can_add_order()
    {
        const string Email = "test";
        const int customerId = 542;
        var (sut, mockrepo) = OrderServiceFactory.Generate();
        mockrepo.Setup(t =>
                t.Get(It.IsAny<Expression<Func<Customer, bool>>>(),
                    It.IsAny<Expression<Func<Customer, Customer>>>()))
            .ReturnsAsync((Expression<Func<Customer, bool>> func,
                    Expression<Func<Customer, Customer>> pro) =>
                new[] {new Customer {Email = Email, Id = customerId}}.Where(func.Compile()).Select(pro.Compile())
                    .FirstOrDefault());

        var result = await sut.AddOrder(new CreateOrderRequest {CustomerEmail = Email, Total = 100});

        mockrepo.Verify(t => t.Add(It.Is<Order>(x => x.CustomerId == customerId &&
                                                     x.Id == result &&
                                                     x.Created > DateTime.Now.AddSeconds(-2) &&
                                                     x.Created < DateTime.Now.AddDays(2)
                                                     && x.Total == 100)), Times.Once);
    }
}