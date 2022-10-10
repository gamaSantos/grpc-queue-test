using Customers.Domain.Commands;

namespace Customers.Domain.Handlers;

public interface ICreateCustomerHandler
{
    Task<ICommandResult<Customer>> Create(CreateCustomer command);
}
