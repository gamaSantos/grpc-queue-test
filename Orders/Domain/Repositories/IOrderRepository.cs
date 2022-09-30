namespace GrpcQueueTest.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task<bool> Insert(Order order);
}