using Customers.Domain.Commands;
using Customers.Domain.Repositories;

namespace Customers.Domain.Handlers;

public class CreateAddressHandler : ICreateAddressHandler
{
    private ICustomerRepository _repository;

    public CreateAddressHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult<Customer>> Create(CreateAddress command)
    {
        var getCustomerResult = await _repository.Get(command.Id);
        return await getCustomerResult.Match(errors => Task.FromResult(CommandResult.CreateFailed<Customer>(errors)),
        customer => CreateAddress(command, customer));

        Task<ICommandResult<Customer>> CreateAddress(CreateAddress command, Customer customer)
        {
            var address = Address.Create(command.StreetName, command.Number, command.Observation);
            customer.AddAddress(address);
            return _repository.Update(customer);
        }
    }
}