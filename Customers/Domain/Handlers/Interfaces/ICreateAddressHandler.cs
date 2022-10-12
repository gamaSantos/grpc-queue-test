using Customers.Domain.Commands;

namespace Customers.Domain.Handlers;

public interface ICreateAddressHandler
{
    Task<ICommandResult<Customer>> Create(CreateAddress command);
}
