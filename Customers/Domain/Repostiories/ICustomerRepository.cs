using Customers.Domain.Models;

namespace Customers.Domain.Repositories;

public interface ICustomerRepository
{
    Task<ICommandResult<CustomerId>> Create(Customer customer);
    Task<ICommandResult<Customer>> Update(Customer customer);
    Task<ICommandResult<Customer>> Get(CustomerId id);
}