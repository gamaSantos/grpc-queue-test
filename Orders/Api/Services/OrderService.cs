using System;
using Grpc.Core;
using api;

namespace api.Services;

public class OrderService : Order.OrderBase
{
    public override Task<CreateOrderResponse> Create(CreateOrderRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}