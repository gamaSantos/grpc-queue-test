using Customers.Domain.Models;

namespace Customers.Domain.Repositories;

public interface ICustomerRepository
{
    ICommandResult<CustomerId> Create(Customer customer);
    ICommandResult<Customer> Update(Customer customer);
    Customer Get(CustomerId id);
}