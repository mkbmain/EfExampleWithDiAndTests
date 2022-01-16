using ServiceLayer.Models;

namespace ServiceLayer.Service.Customer;

public interface ICustomerService : IBaseService<DataLayer.Customer>
{
     Task<int> AddCustomer(CreateCustomerRequest request);
     Task<CustomerResponse> GetCustomerByEmail(string email);
     Task<CustomerResponse> GetCustomerById(int id);
}