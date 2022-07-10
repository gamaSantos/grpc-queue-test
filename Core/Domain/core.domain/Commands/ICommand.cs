namespace GrpcQueueTest.Core.Domain.Comands;

public interface ICommand
{
    ValidationResult IsValid();
}
