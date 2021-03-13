using System;

namespace Application.Product.DTOs
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public int Count { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
    }
}
