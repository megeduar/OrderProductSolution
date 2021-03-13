using FluentValidation;

namespace Application.Order.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(v => v.OrderId).NotEmpty();
        }
    }
}
