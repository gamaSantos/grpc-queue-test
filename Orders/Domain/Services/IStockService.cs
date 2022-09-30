using GrpcQueueTest.Orders.Domain.Models.Stock;

namespace GrpcQueueTest.Orders.Domain.Services;

public interface IStockService
{
    Task<Withdraw> WithdrawFromStock(WithdrawItems items);
}