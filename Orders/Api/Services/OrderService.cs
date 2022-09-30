using System;
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

    public override Task<Creat eOrderResponse> Create(CreateOrderRequest request, ServerCallContext context)
    {
        var command = new CreateOrderCommand
        {
            CustomerId = new Guid(request.CustomerId),
            AdressId = new Guid(request.AddressId),
            PhoneNumber = request.PhoneNumber,
            PhoneRegion = request.PhoneRegion,
            Pizzas = new List<CreatePizzaCommand>()
        };
        var result = _createOrderCommandHandler.Handle(command);
        return result;
    }
}