using Customers.Domain.Models;

namespace Customers.Domain.Commands;

public class CreateCustomer
{
    public CreateCustomer(Name name, Telephone telephone)
    {
        Name = name;
        Telephone = telephone;
    }

    public Name Name { get; }
    public Telephone Telephone { get; }
}