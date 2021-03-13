using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Order.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }

        public class Handler : IRequestHandler<DeleteOrderCommand>
        {
            private readonly IOrderDbContext _context;

            public Handler(IOrderDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Orders.FindAsync(request.OrderId);

                if (entity == null)
                    throw new NotFoundException("Order", request.OrderId);

                if (entity.State == Domain.Entities.OrderState.CONFIRMED)
                    throw new InvalidOperationException("Operation not valid. The Order is CONFIRMED");

                _context.Orders.Remove(entity);

                int value = await _context.SaveChangesAsync(cancellationToken);

                if (value > 0)
                    return Unit.Value;

                throw new Application.ExceptionHandler.
                    ExceptionHandler(System.Net.HttpStatusCode.Conflict,
                    new { message = "Delete order conflict" });
            }
        }
    }
}
