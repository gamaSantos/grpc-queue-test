using System.Collections.Generic;
using System.Linq;
using Customers.Domain.Models;

namespace Customers.Domain;
public class Customer
{
    private IDictionary<AddressId, Address> _address;

    private Customer(CustomerId id, Name fullName, Telephone phone)
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

    public static ICommandResult<Customer> TryCreate(Name fullname, Telephone phone)
    {
        var validator = CreateComponentsValidator(fullname, phone);
        return validator.CreateCommand(new Customer(
            new CustomerId(Guid.NewGuid()),
            fullname,
            phone
        ));
    }

    public static ICommandResult<Customer> Load(CustomerId id, Name name, Telephone phone)
    {
        var validator = CreateComponentsValidator(name, phone);
        validator.AddRule(id.IsValid(), "id must not be empty");
        return validator.CreateCommand(new Customer(id, name, phone));
    }
    private static Validator CreateComponentsValidator(Name fullname, Telephone phone)
    {
        var validator = Validator.Create();
        validator.AddRule(fullname.IsValid(), "name is not valid");
        validator.AddRule(phone.IsValid(), "fix the phone number");
        return validator;
    }

    public ICommandResult AddAddress(Address address) => CommandResult.CreateFromBool(_address.TryAdd(address.Id, address));

    public ICommandResult RemoveAddress(AddressId id) => CommandResult.CreateFromBool(_address.Remove(id));
}
