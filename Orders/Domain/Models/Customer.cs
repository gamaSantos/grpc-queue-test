namespace GrpcQueueTest.Orders.Domain.Models;

public class Customer : IDeliverable
{
    public Customer(IEntityId id, Telephone phone, IAddress adress)
    {
        Id = id;
        Phone = phone;
        Address = adress;
    }

    public IEntityId Id { get; }
    public Telephone Phone { get; }
    public IAddress Address { get; }
    public string GetAdressLine() => Address.ToString();

    public IDeliverable AsDeliverable() => (IDeliverable)this;
}