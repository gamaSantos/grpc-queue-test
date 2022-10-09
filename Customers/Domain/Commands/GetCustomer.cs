using Customers.Domain.Models;

namespace Customers.Domain.Commands;

public class GetCustomer
{
    public GetCustomer(CustomerId id)
    {
        Id = id;
    }

    public CustomerId Id { get; }
}