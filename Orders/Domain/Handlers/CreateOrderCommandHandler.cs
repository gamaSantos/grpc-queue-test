using GrpcQueueTest.Orders.Domain.Commands;
using GrpcQueueTest.Orders.Domain.Models;
using GrpcQueueTest.Orders.Domain.Models.Stock;
using GrpcQueueTest.Orders.Domain.Repositories;
using GrpcQueueTest.Orders.Domain.Services;

namespace GrpcQueueTest.Orders.Domain.Handlers;

public class CreateOrderCommandHandler : ICreateOrderCommandHandler
{
    private readonly IStockService _stockService;
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerService _customerService;

    public CreateOrderCommandHandler(IStockService stockService,
                                     IOrderRepository orderRepository,
                                     ICustomerService customerService)
    {
        _stockService = stockService;
        _orderRepository = orderRepository;
        _customerService = customerService;
    }

    public async Task<ICommandResult<Order>> Handle(CreateOrderCommand command)
    {
        var getCustomerResult = await GetCustomer(command.CustomerId);
        var buildPizzasResult = await BuildPizzas(command);
        return buildPizzasResult.Match(
            errors => CommandResult.CreateFailed<Order>(errors),
            pizzas =>
            {
                return getCustomerResult.Match(
                    errors => CommandResult.CreateFailed<Order>(errors),
                    customer =>
                    {
                        var createOrderResult = Order.Create(pizzas, customer);
                        createOrderResult.Do(async order => await _orderRepository.Insert(order));
                        return createOrderResult;
                    }
                );
            }
        );
    }

    private async Task<ICommandResult<IEnumerable<Pizza>>> BuildPizzas(CreateOrderCommand command)
    {
        var resultCollection = new CommandResultCollection<Pizza>();
        var withdraw = await GetWithdrawFromStock(command);
        if (withdraw.IsCompleted == false) return CommandResult.CreateFailed<IEnumerable<Pizza>>("Some flavors are missing in stock");
        foreach (var createPizzaCommand in command.Pizzas)
        {
            var flavors = createPizzaCommand.Flavors
                    .Select(flavorSku => withdraw.Items.First(pc => pc.Sku == flavorSku))
                    .Select(si => new Flavor(si.Sku, si.BasePrice));
            resultCollection.Add(Pizza.Create(flavors));
        }
        return resultCollection.Flatten();
    }

    private async Task<Withdraw> GetWithdrawFromStock(CreateOrderCommand command)
    {
        var neededSku = command.Pizzas
                            .SelectMany(p => p.Flavors)
                            .GroupBy(i => i)
                            .Select(g => new { sku = g.Key, quantity = g.Count() });
        var withdrawItems = WithdrawItems.CreateEmpty();
        foreach (var item in neededSku)
        {
            withdrawItems.TryAddItem(item.sku, (uint)item.quantity);
        }
        return await _stockService.WithdrawFromStock(withdrawItems);
    }

    private async Task<ICommandResult<Customer>> GetCustomer(Guid? customerId)
    {
        var customer = await _customerService.GetById(customerId);
        if (customer == null) return CommandResult.CreateFailed<Customer>("Customer not found");
        return CommandResult.CreateSuccess(customer);
    }
}