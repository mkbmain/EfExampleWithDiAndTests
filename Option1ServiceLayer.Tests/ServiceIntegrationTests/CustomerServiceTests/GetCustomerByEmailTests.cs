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

public class GetCustomerByEmailTests : BaseIntegrationTests<ServiceLayer.Service.Customer.CustomerService>
{
    [Fact]
    public async Task Ensure_we_can_GetCustomerByEmail()
    {
        const string Email = "mike@gmail.com";
        var goodCustomer = new Customer
        {
            Email = Email,
            DateOfBirth = DateTime.Now.Date.AddDays(-2662),
            Name = "mike",
        };

        SimpleDbContext.Customers.Add(goodCustomer);
        await SimpleDbContext.SaveChangesAsync();

        var customer = await Sut.GetCustomerByEmail(Email);

        customer.Email.ShouldBe(goodCustomer.Email);
        customer.Name.ShouldBe(goodCustomer.Name);
        customer.Id.ShouldBe(goodCustomer.Id);
        customer.Dob.ShouldBe(goodCustomer.DateOfBirth);
    }
}