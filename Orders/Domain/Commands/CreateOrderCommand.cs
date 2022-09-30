namespace GrpcQueueTest.Orders.Domain.Commands;

public class CreateOrderCommand : ICommand
{
    public Guid? CustomerId { get; set; }
    public Guid? AdressId { get; set; }
    public string? PhoneRegion { get; set; }
    public string? PhoneNumber { get; set; }

    public List<CreatePizzaCommand> Pizzas { get; set; } = new List<CreatePizzaCommand>();

    public ICommandResult<Telephone> TryCreatePhone()
    {
        var number = PhoneNumber ?? string.Empty;
        var region = PhoneRegion ?? string.Empty;
        var phone = new Telephone(number, region);
        var validation = Validator.Create(phone);
        validation.AddRule(x => x.IsValid(), "couldn't recognize the contact information");

        return validation.CreateCommand(phone);
    }

    public ValidationResult IsValid()
    {
        var validator = Validator.Create(this);
        validator.AddRule(x => x.Pizzas.Any(), "An order must have at least one pizza");
        foreach (var pizza in Pizzas) validator.Append(pizza.IsValid());
        return validator.Validate();
    }
}

public class CreatePizzaCommand : ICommand
{
    public List<string> Flavors { get; set; } = new List<string>();

    public ValidationResult IsValid()
    {
        var valiadtionBuilder = Validator.Create(this);
        valiadtionBuilder.AddRule(x => x.Flavors.Any(), "A pizza must have at least one flavor");
        return new ValidationResult();
    }
}