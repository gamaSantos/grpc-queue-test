using Customers.Domain;
using Customers.Domain.Models;
using Customers.Domain.Repositories;

namespace Customers.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public ICommandResult<CustomerId> Create(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Customers.Domain.Customer Get(CustomerId id)
    {
        throw new NotImplementedException();
    }

    public ICommandResult<Customers.Domain.Customer> Update(Customer customer)
    {
        throw new NotImplementedException();
    }
}