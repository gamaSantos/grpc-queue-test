using System.Collections.Generic;
using System.Linq;
using Customers.Domain.Models;

namespace Customers.Domain;
public class Customer
{
    private IDictionary<AddressId, Address> _address;

    public Customer(CustomerId id, Name fullName, Telephone phone)
    {
        Id = id;
        FullName = fullName;
        Phone = phone;
        _address = new Dictionary<AddressId, Address>();
    }

    public CustomerId Id { get; }
    public Name FullName { get; }
    public Telephone Phone { get; }
    public IReadOnlyCollection<Address> Addresses => _address.Values.ToList().AsReadOnly();

    public ICommandResult AddAddress(Address address) => CommandResult.CreateFromBool(_address.TryAdd(address.Id, address));

    public ICommandResult RemoveAddress(AddressId id) => CommandResult.CreateFromBool(_address.Remove(id));
}
