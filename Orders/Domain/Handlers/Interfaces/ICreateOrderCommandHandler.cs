using GrpcQueueTest.Orders.Domain.Commands;

namespace GrpcQueueTest.Orders.Domain.Handlers;

public interface ICreateOrderCommandHandler
{
    Task<ICommandResult<Order>> Handle(CreateOrderCommand command);
}
