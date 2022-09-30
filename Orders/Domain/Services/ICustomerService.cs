using GrpcQueueTest.Orders.Domain.Models;

namespace GrpcQueueTest.Orders.Domain.Services;

public interface ICustomerService
{
    Task<Customer> GetById(Guid? customerId);
}