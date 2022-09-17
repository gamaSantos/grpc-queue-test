namespace GrpcQueueTest.Core.Domain;

public interface IEntityId
{
    Guid Value { get; }
    bool IsValid();
}