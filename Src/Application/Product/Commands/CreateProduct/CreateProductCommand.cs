using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string Desciption { get; set; }
        public int Count { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }

        public class Handler : IRequestHandler<CreateProductCommand>
        {
            private readonly IOrderDbContext _context;

            public Handler(IOrderDbContext context, IMediator mediator)
            {
                _context = context;
            }
           

            public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                Domain.Entities.Product entity = new Domain.Entities.Product
                {
                    ProductId = Guid.NewGuid(),
                    UserId = request.UserId,
                    Name = request.Name,
                    Desciption = request.Desciption,
                    Slug = request.Slug,
                    Count = request.Count,
                    Price = request.Price,
                };

                _context.Products.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}

