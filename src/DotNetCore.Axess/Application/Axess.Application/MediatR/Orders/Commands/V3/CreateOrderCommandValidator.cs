namespace Axess.Application.MediatR.Orders.Commands.V3;

using FluentValidation;

/// <summary>
/// 
/// </summary>
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
	public CreateOrderCommandValidator()
	{
		RuleFor(sample => sample.Customer).NotNull();
		RuleFor(sample => sample.LineItems).NotEmpty();
	}

}
