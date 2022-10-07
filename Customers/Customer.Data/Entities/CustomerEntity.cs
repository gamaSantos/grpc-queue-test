using Customers.Domain;
using Customers.Domain.Models;

namespace Customers.Data.Entities;

class CustomerEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string PhoneRegion { get; set; }

    public ICommandResult<Customer> ToDomain() => Customer.Load(
        new CustomerId(Id),
        new Name(FirstName, LastName),
        new Telephone(PhoneNumber, PhoneRegion));

    public static CustomerEntity FromDomain(Customer customer) => new CustomerEntity
    {
        Id = customer.Id.Value,
        FirstName = customer.FullName.FirstName,
        LastName = customer.FullName.LastName,
        PhoneNumber = customer.Phone.PhoneNumber,
        PhoneRegion = customer.Phone.RegionCode
    };
}

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