using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product.Commands.BuyProduct
{
    public class PurchaseProductCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Count { get; set; }

        public class Handler : IRequestHandler<PurchaseProductCommand>
        {
            private readonly IOrderDbContext _context;

            public Handler(IOrderDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(PurchaseProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Products.FindAsync(request.ProductId);

                if (entity == null)
                    throw new NotFoundException("Product", request.ProductId);

                if (entity.Count == 0 || entity.Count < request.Count)
                    throw new AvailabilityException("Product", request.ProductId);

                entity.Count -= request.Count;

                var newOrder = new Domain.Entities.Order
                {
                    OrderId   = Guid.NewGuid(),
                    UserId    = request.UserId,
                    ProductId = entity.ProductId,
                    Date      = DateTime.UtcNow,
                    Count     = request.Count,
                    State     = OrderState.CREATED
                };

                _context.Orders.Add(newOrder);

                int value = await _context.SaveChangesAsync(cancellationToken);

                if (value > 0)
                {
                    return Unit.Value;
                }

                throw new Application.ExceptionHandler.
                    ExceptionHandler(System.Net.HttpStatusCode.Conflict,
                    new { message = "Buy product conflict" });
            }
        }
    }
}
