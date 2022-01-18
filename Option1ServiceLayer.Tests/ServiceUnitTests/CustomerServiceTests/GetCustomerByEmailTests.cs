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

namespace Option1ServiceLayer.Tests.ServiceUnitTests.CustomerServiceTests;

public class GetCustomerByEmailTests
{
    [Fact]
    public async Task Ensure_we_can_GetCustomerByEmail()
    {
        const string Email = "mike@gmail.com";
        var goodCustomer = new Customer
        {
            Id = 542,
            Email = Email,
            DateOfBirth = DateTime.Now.Date.AddDays(-2662),
            Name = "mike",
        };
        var (sut, mockrepo) = CustomerServiceFactory.Generate();
        var list = new List<Customer>() {goodCustomer, new Customer{ Email = Email+"test", DateOfBirth = DateTime.Now.Date,Name = "john"}};
        mockrepo.Setup(t =>
                t.Get(It.IsAny<Expression<Func<Customer, bool>>>(), It.IsAny<Expression<Func<Customer, CustomerResponse>>>()))
            .ReturnsAsync((Expression<Func<Customer, bool>> func,
                Expression<Func<Customer, CustomerResponse>> pro) => list.Where(func.Compile()).Select(pro.Compile()).FirstOrDefault() );

        var customer = await sut.GetCustomerByEmail(Email);
        
        customer.Email.ShouldBe(goodCustomer.Email);
        customer.Name.ShouldBe(goodCustomer.Name);
        customer.Id.ShouldBe(goodCustomer.Id);
        customer.Dob.ShouldBe(goodCustomer.DateOfBirth);
    }
}

