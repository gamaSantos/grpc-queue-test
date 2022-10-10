using Customers.Domain.Commands;
using Customers.Domain.Repositories;

namespace Customers.Domain.Handlers;

public class CreateCustomerHandler : ICreateCustomerHandler
{
    private ICustomerRepository _repository;

    public CreateCustomerHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<ICommandResult<Customer>> Create(CreateCustomer command)
    {
        var createResult = Customer.TryCreate(command.Name, command.Telephone);
        return await createResult.Match(errors => Task.FromResult(createResult),
        async customer => await SaveCustomer(customer, createResult));
    }

    async Task<ICommandResult<Customer>> SaveCustomer(Customer customer, ICommandResult<Customer> createResult)
    {
        var saveResult = await _repository.Create(customer);
        return saveResult.Match(
            errors => CommandResult.CreateFailed<Customer>(errors),
            id => createResult);
    }
}