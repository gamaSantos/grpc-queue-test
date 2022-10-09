using Customers.Domain.Commands;
using Customers.Domain.Repositories;

namespace Customers.Domain.Handlers;

public class CreateAddressHandler
{
    private ICustomerRepository _repository;
    public async Task<ICommandResult<Customer>> Create(CreateAddress command)
    {
        var getCustomerResult = await _repository.Get(command.Id);
        return getCustomerResult.Match(errors =>
        {
            return CommandResult.CreateFailed<Customer>(errors);
        },
        customer =>
        {
            var address = Address.Create(command.StreetName, command.Number, command.Observation);
            customer.AddAddress(address);
            return _repository.Update(customer);

        });
    }
}