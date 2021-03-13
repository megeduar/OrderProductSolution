using FluentValidation;

namespace Application.Product.Commands.UpdateProduct
{
    public class UpdateProductCommmandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommmandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(v => v.ProductId)
                .NotEmpty()
                .WithMessage("Product ID is required.");
        }
    }
}
