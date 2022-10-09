using Customers.Domain.Models;

namespace Customers.Domain.Commands;

public class CreateAddress
{
    public CreateAddress(CustomerId id, string streetName, string number, string? observation)
    {
        Id = id;
        StreetName = streetName;
        Number = number;
        Observation = observation;
    }

    public CustomerId Id { get; }
    public string StreetName { get; }
    public string Number { get; }
    public string? Observation { get; }
}