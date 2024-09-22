using DropShipping.Domain.Models;
using DropShipping.Dto.ProductDtos;

namespace DropShipping.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }

        public static Product ToProduct(this AddProductDto productDto)
        {
            return new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                Quantity = productDto.Quantity
            };
        }
    }
}
