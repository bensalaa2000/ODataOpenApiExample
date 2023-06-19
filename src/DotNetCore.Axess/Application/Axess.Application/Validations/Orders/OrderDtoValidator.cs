using Axess.Application.Models;
using FluentValidation;

namespace Axess.Application.Validations.Orders;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
	public OrderDtoValidator()
	{
		RuleFor(sample => sample.Customer).NotNull();
		RuleFor(sample => sample.Code).NotEmpty();
		/*RuleFor(sample => sample.EmailAddress).EmailAddress();
        RuleFor(sample => sample.RegexField).Matches(@"(\d{4})-(\d{2})-(\d{2})");

        RuleFor(sample => sample.ValueInRange).GreaterThanOrEqualTo(5).LessThanOrEqualTo(10);
        RuleFor(sample => sample.ValueInRangeExclusive).GreaterThan(5).LessThan(10);

        // WARNING: Swashbuckle implements minimum and maximim as int so you will loss fraction part of float and double numbers
        RuleFor(sample => sample.ValueInRangeFloat).InclusiveBetween(1.1f, 5.3f);
        RuleFor(sample => sample.ValueInRangeDouble).ExclusiveBetween(2.2, 7.5f);*/
	}
}
