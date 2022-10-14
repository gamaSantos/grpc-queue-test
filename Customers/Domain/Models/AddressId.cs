using System;

namespace Customers.Domain.Models;
public class AddressId : BaseId
{
    public AddressId(Guid value) : base(value)
    {
    }
    public static implicit operator Guid(AddressId id) => id.Value;
    public static implicit operator AddressId(Guid id) => new(id);

    public override string ToString() => Value.ToString("N");

}