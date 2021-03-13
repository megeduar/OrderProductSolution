using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Order.UpdateOrder
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public OrderState State { get; set; }

        public class Handler : IRequestHandler<UpdateOrderCommand>
        {
            private readonly IOrderDbContext _context;

            public Handler(IOrderDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Orders.
                    FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);

                if (entity == null)
                    throw new NotFoundException("Order", request.OrderId);


                entity.State = request.State;

                int value = await _context.SaveChangesAsync(cancellationToken);

                if (value > 0)
                    return Unit.Value;

                throw new Application.ExceptionHandler.
                    ExceptionHandler(System.Net.HttpStatusCode.Conflict,
                    new { message = "Update order conflict" });
            }
        }
    }
}
