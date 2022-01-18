using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Moq;
using ServiceLayer.Models;
using Shouldly;
using Xunit;

namespace Option1ServiceLayer.Tests.ServiceIntegrationTests.CustomerServiceTests;

public class AddCustomerTests : BaseIntegrationTests<ServiceLayer.Service.Customer.CustomerService>
{
    [Fact]
    public async Task Ensure_we_can_addCustomer()
    {
        var request = new CreateCustomerRequest
        {
            City = "cityTe",
            PostCode = "15236526",
            Email = "grwh@hrwh.com",
            BuildingName = "name",
            BuildingNumber = "4yt34",
            Dob = DateTime.Now.AddDays(-555).Date,
            Name = "mikeb",
            Street = "streetName"
        };


        await Sut.AddCustomer(request);
        var response = SimpleDbContext.Customers.FirstOrDefault();
        response.ShouldNotBeNull();
        Assert.Equal(request.Email, response.Email);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(1, response.CustomerAddresses.Count);
        Assert.Equal(request.City, response.CustomerAddresses.First().City);
        Assert.Equal(request.Street, response.CustomerAddresses.First().Street);
        Assert.Equal(request.BuildingName, response.CustomerAddresses.First().BuildingName);
        Assert.Equal(request.BuildingNumber, response.CustomerAddresses.First().BuildingNumber);
        Assert.Equal(request.PostCode, response.CustomerAddresses.First().PostCode);
        Assert.Equal(request.Email, response.Email);
        Assert.Equal(request.Dob, response.DateOfBirth);
    }
}