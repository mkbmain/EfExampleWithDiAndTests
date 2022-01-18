using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataLayer;
using Moq;
using ServiceLayer.Models;
using Shouldly;
using Xunit;

namespace Option1ServiceLayer.Tests.ServiceIntegrationTests.CustomerServiceTests;

public class GetCustomerByIdTests : BaseIntegrationTests<ServiceLayer.Service.Customer.CustomerService>
{
    [Fact]
    public async Task Ensure_we_can_GetCustomerById()
    {
        const string Email = "mike@gmail.com";
        var goodCustomer = new Customer
        {
            Email = Email,
            DateOfBirth = DateTime.Now.Date.AddDays(-2662),
            Name = "mike",
        };

        await SimpleDbContext.Customers.AddAsync(goodCustomer);
        await SimpleDbContext.SaveChangesAsync();
        var customer = await Sut.GetCustomerById(goodCustomer.Id);

        customer.Email.ShouldBe(goodCustomer.Email);
        customer.Name.ShouldBe(goodCustomer.Name);
        customer.Id.ShouldBe(goodCustomer.Id);
        customer.Dob.ShouldBe(goodCustomer.DateOfBirth);
    }
}