using Customers.Domain.Commands;

namespace Customers.Domain.Handlers;

public interface IGetCustomerHandler
{
    Task<ICommandResult<Customer>> Get(GetCustomer query);
}
