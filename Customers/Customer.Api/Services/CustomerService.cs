using Customers.Domain.Commands;
using Customers.Domain.Handlers;
using Customers.Domain.Models;
using Grpc.Core;
using GrpcQueueTest.Core.Domain;
using GrpcQueueTest.Customers.Api;
using static GrpcQueueTest.Customers.Api.Customer;

namespace GrpcQueueTest.Customer.Api.Services;

public class CustomerService : CustomerBase
{
    private readonly ICreateCustomerHandler _createHandler;
    private readonly IGetCustomerHandler _getHandler;

    public CustomerService(ICreateCustomerHandler createHandler, IGetCustomerHandler getHandler)
    {
        _createHandler = createHandler;
        _getHandler = getHandler;
    }

    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var fullname = new Name(request.FirstName, request.LastName);
        var telephone = new Telephone(request.PhoneNumber, request.PhoneRegion);
        var createCommand = new CreateCustomer(fullname, telephone);
        var result = await _createHandler.Create(createCommand);

        return result.Match(errors =>
        {
            var response =
            new CreateResponse
            {
                Id = Guid.Empty.ToString(),
                Success = false
            };
            response.Errors.AddRange(errors);
            return response;
        },
        customer => new CreateResponse
        {
            Id = customer.Id.ToString(),
            Success = true
        });
    }


    public override Task<GetResponse> Get(GetRequest request, ServerCallContext context)
    {
        return base.Get(request, context);
    }

}