namespace GrpcQueueTest.Core.Domain;

public abstract class BaseId
{
    protected BaseId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }
    public bool IsValid() => Value != Guid.Empty;
}