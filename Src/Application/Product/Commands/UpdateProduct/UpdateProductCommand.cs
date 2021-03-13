using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public int Count { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }

        public class Handler : IRequestHandler<UpdateProductCommand>
        {
            private readonly IOrderDbContext _context;

            public Handler(IOrderDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Products.
                    FirstOrDefaultAsync(x => x.ProductId == request.ProductId, cancellationToken);

                if (entity == null)
                    throw new NotFoundException("Product", request.ProductId);

                entity.Name = request.Name;
                entity.Desciption = request.Desciption;
                entity.Count = request.Count;
                entity.Slug = request.Slug;
                entity.Price = request.Price;

                int value = await _context.SaveChangesAsync(cancellationToken);

                if (value > 0)
                    return Unit.Value;

                throw new Application.ExceptionHandler.
                    ExceptionHandler(System.Net.HttpStatusCode.Conflict,
                    new { message = "Update Product conflict" });
            }
        }
    }
}
