using Customers.Data.Repositories;
using Customers.Domain.Handlers;
using Customers.Domain.Repositories;
using GrpcQueueTest.Customer.Api.Services;

var builder = WebApplication.CreateBuilder(args);

AddCustomerServices(builder);

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<CustomerService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

static void AddCustomerServices(WebApplicationBuilder builder)
{
    builder.Services.AddTransient<ICustomerRepository>(_ =>
    {
        var connectionString = builder.Configuration.GetConnectionString("customer");
        return new CustomerRepository(connectionString);
    });
    builder.Services.AddTransient<ICreateCustomerHandler, CreateCustomerHandler>();
    builder.Services.AddTransient<ICreateAddressHandler, CreateAddressHandler>();
    builder.Services.AddTransient<IGetCustomerHandler, GetCustomerHandler>();
}