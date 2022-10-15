using GrpcQueueTest.Orders.Domain.Commands;
using GrpcQueueTest.Orders.Domain.Models;
using GrpcQueueTest.Orders.Domain.Repositories;
using GrpcQueueTest.Orders.Domain.Services;

namespace GrpcQueueTest.Orders.Domain.Handlers;

public class CreateOrderCommandHandler : ICreateOrderCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerService _customerService;

    public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                     ICustomerService customerService)
    {
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
        foreach (var createPizzaCommand in command.Pizzas)
        {
            //TODO add price in command
            var flavors = createPizzaCommand.Flavors.Select(flavorSku => new Flavor(flavorSku, 0));
            resultCollection.Add(Pizza.Create(flavors));
        }
        return resultCollection.Flatten();
    }

    private async Task<ICommandResult<Customer>> GetCustomer(Guid? customerId)
    {
        var customer = await _customerService.GetById(customerId);
        if (customer == null) return CommandResult.CreateFailed<Customer>("Customer not found");
        return CommandResult.CreateSuccess(customer);
    }
}