using Grpc.Core;
using GrpcQueueTest.Orders.Api;
using GrpcQueueTest.Orders.Domain.Commands;
using GrpcQueueTest.Orders.Domain.Handlers;

namespace Api.Services;

public class OrderService : Order.OrderBase
{
    private ICreateOrderCommandHandler _createOrderCommandHandler;

    public OrderService(ICreateOrderCommandHandler createOrderCommandHandler)
    {
        _createOrderCommandHandler = createOrderCommandHandler;
    }

    public override async Task<CreateOrderResponse> Create(CreateOrderRequest request, ServerCallContext context)
    {
        var command = new CreateOrderCommand
        {
            CustomerId = new Guid(request.CustomerId),
            AdressId = new Guid(request.AddressId),
            PhoneNumber = request.PhoneNumber,
            PhoneRegion = request.PhoneRegion,
            Pizzas = request.Pizzas.Select(x => new CreatePizzaCommand() { Flavors = x.Flavors.ToList() }).ToList()
        };
        var result = await _createOrderCommandHandler.Handle(command);
        return result.Match(
            errors => new CreateOrderResponse() { Success = false, Errors = string.Join(',', errors) },
            order => new CreateOrderResponse() { Success = true, OrderId = order.Id.Value.ToString() });
    }
}