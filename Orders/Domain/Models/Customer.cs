namespace GrpcQueueTest.Orders.Domain.Models;

public class Customer : IDeliverable
{
    public Customer(IEntityId id, Telephone phone, Address adress)
    {
        Id = id;
        Phone = phone;
        Adress = adress;
    }

    public IEntityId Id { get; }
    public Telephone Phone { get; }
    public Address Adress { get; }
    public string GetAdressLine() => Adress.ToString();

    public IDeliverable AsDeliverable() => (IDeliverable)this;
}