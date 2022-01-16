using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Moq;
using ServiceLayer.Models;
using Xunit;

namespace Option1ServiceLayer.Tests.ServiceTests.CustomerServiceTests;

public class AddCustomerTests
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

        var (sut, mockrepo) = CustomerServiceFactory.Generate();


        await sut.AddCustomer(request);
        mockrepo.Verify(e => e.Add(It.Is<Customer>(t => t.Email == request.Email &&
                                                        t.Name == request.Name &&
                                                        t.CustomerAddresses.Count == 1 &&
                                                        t.CustomerAddresses.First().City == request.City &&
                                                        t.CustomerAddresses.First().Street == request.Street &&
                                                        t.CustomerAddresses.First().BuildingName ==
                                                        request.BuildingName &&
                                                        t.CustomerAddresses.First().BuildingNumber ==
                                                        request.BuildingNumber &&
                                                        t.CustomerAddresses.First().PostCode == request.PostCode &&
                                                        t.Email == request.Email &&
                                                        t.DateOfBirth == request.Dob)), Times.Once());
    }
}