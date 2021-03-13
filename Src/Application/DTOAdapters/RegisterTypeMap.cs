using Application.Product.DTOs;
using AutoMapper;

namespace Application.DTOAdapters
{
    public class RegisterTypeMap : Profile
    {
        public RegisterTypeMap()
        {
            SetMap();
        }

        private void SetMap()
        {
            var mappingExpression = CreateMap<Domain.Entities.Product, ProductDto>();
        }
    }
}
