using FluentValidation;

namespace Application.Order.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(v => v.OrderId)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(v => v.State)
                .NotEmpty()
                .WithMessage("Order State is required.");
        }
    }
}
