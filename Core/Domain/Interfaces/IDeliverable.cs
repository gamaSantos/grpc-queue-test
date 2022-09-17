namespace GrpcQueueTest.Core.Domain;

public interface IDeliverable
{
    public string GetAdressLine();
    public Telephone Phone { get; }
    public Address Adress { get; }
}