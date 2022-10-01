using System;
using Customers.Domain.Models;

namespace Customers.Domain;

public class Address
{

    private Address(AddressId id, string streetName, string number, string? observation = null)
    {
        Id = id;
        StreetName = streetName;
        Number = number;
        Observation = observation;
    }
    public AddressId Id { get; }
    public string StreetName { get; }
    public string Number { get; }
    public string? Observation { get; }

    internal static Address Create(string streetName, string number, string? observation = null) => new(new AddressId(Guid.NewGuid()), streetName, number, observation);

    internal static Address Load(AddressId id, string streetName, string number, string? observation = null) => new(id, streetName, number, observation);

    public override string ToString()
    {
        return $"{StreetName}, {Number}{(string.IsNullOrWhiteSpace(Observation) ? string.Empty : " " + Observation)}";
    }

    internal bool IsValid() => string.IsNullOrWhiteSpace(StreetName) == false && string.IsNullOrWhiteSpace(Number) == false;

}