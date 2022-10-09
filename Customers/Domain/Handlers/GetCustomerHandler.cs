using Customers.Domain.Commands;
using Customers.Domain.Repositories;

namespace Customers.Domain.Handlers;

public class GetCustomerHandler
{
    private ICustomerRepository _repository;

    public GetCustomerHandler(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public Task<ICommandResult<Customer>> Get(GetCustomer query)
    {
        return _repository.Get(query.Id);
    }
}