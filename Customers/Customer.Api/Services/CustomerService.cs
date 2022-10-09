using Grpc.Core;
using GrpcQueueTest.Customers.Api;
using static GrpcQueueTest.Customers.Api.Customer;

namespace GrpcQueueTest.Customer.Api.Services;

public class CustomerService : CustomerBase
{
    public override Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        return base.Create(request, context);
    }


    public override Task<GetResponse> Get(GetRequest request, ServerCallContext context)
    {
        return base.Get(request, context);
    }

}