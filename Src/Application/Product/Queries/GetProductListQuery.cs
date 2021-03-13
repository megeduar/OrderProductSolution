using Application.Common.Interfaces;
using Application.Product.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Product.Queries
{
    public class GetProductListQuery : IRequest<IList<ProductDto>>
    {
        public class GetProductQueryHandler :
        IRequestHandler<GetProductListQuery, IList<ProductDto>>
        {
            private readonly IOrderDbContext _context;
            private readonly IMapper _mapper;

            public GetProductQueryHandler(IOrderDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IList<ProductDto>> Handle(
                GetProductListQuery request, CancellationToken cancellationToken)
            {
                var collection = await _context.Products
                    .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return collection;
            }
        }
    }
}