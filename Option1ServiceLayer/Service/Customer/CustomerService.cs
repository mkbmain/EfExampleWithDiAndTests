using System.Linq.Expressions;
using DataLayer;
using ServiceLayer.Models;
using SimpleRepo.Repo;

namespace ServiceLayer.Service.Customer;

public class CustomerService : BaseService<DataLayer.Customer>, ICustomerService
{
    public CustomerService(IRepo<ExampleDbContext> repo) : base(repo)
    {
    }

    public Task<CustomerResponse> GetCustomerByEmail(string email)
    {
        return CustomerResponseByX(t => t.Email == email);
    }

    public Task<CustomerResponse> GetCustomerById(int id)
    {
        return CustomerResponseByX(t => t.Id == id);
    }

    protected Task<CustomerResponse> CustomerResponseByX(Expression<Func<DataLayer.Customer, bool>> func)
    {
        return Repo.Get(func, arg => new CustomerResponse
        {
            Dob = arg.DateOfBirth,
            Email = arg.Email,
            Id = arg.Id,
            Name = arg.Name
        });
    }

    public async Task<int> AddCustomer(CreateCustomerRequest request)
    {
        var customer = new DataLayer.Customer
        {
            Email = request.Email,
            DateOfBirth = request.Dob,
            Name = request.Name,
        };
        customer.CustomerAddresses.Add(new CustomerAddress
        {
            BuildingName = request.BuildingName,
            BuildingNumber = request.BuildingNumber,
            Street = request.Street,
            City = request.City,
            PostCode = request.PostCode
        });
        await Repo.Add(customer);
        return customer.Id;
    }
}