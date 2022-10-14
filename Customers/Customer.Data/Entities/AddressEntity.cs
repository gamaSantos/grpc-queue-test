using Customers.Domain;
using Customers.Domain.Models;

namespace Customers.Data.Entities;

class AddressEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string StreetName { get; set; }
    public string Number { get; set; }
    public string? Observation { get; set; }

    public static AddressEntity FromDomain(CustomerId customerId, Address address) => new AddressEntity()
    {
        Id = address.Id,
        CustomerId = customerId,
        StreetName = address.StreetName,
        Number = address.Number,
        Observation = address.Observation
    };

}