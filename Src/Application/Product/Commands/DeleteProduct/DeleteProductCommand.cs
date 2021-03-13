using Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;

namespace Application.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid ProductId { get; set; }

        public class Handler : IRequestHandler<DeleteProductCommand>
        {
            private readonly IOrderDbContext _context;

            public Handler(IOrderDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Products.FindAsync(request.ProductId);

                if (entity == null)
                    throw new NotFoundException("Product", request.ProductId);

                _context.Products.Remove(entity);

                int value = await _context.SaveChangesAsync(cancellationToken);

                if (value > 0)
                    return Unit.Value;

                throw new Application.ExceptionHandler.
                    ExceptionHandler(System.Net.HttpStatusCode.Conflict, 
                    new { message = "Delete product conflict" });
            }
        }
    }
}
