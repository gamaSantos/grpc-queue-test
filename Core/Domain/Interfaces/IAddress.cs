namespace GrpcQueueTest.Core.Domain;
public interface IAddress
{
    string StreetName { get; }
    string Number { get; }
    string? Observation { get; }

    string ToString();
}