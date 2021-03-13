using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IOrderDbContext _context;

        public CreateProductCommandValidator(IOrderDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MustAsync(BeUniqueProduct).WithMessage("The specified product already exist.");

            RuleFor(v => v.UserId)
                .NotEmpty()
                .WithMessage("User is required.");

            RuleFor(v => v.Desciption)
                .MaximumLength(100)
                .WithMessage("The description must be less than 100 characters");

            RuleFor(v => v.Price)
                .NotEmpty()
                .WithMessage("Price is required.");
        }

        public Task<bool> BeUniqueProduct(string name, CancellationToken cancellationToken)
        {
            return _context.Products.AllAsync(n => n.Name != name);
        }

    }
}
