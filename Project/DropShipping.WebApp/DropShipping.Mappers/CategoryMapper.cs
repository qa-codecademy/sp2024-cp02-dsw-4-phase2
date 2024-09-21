
using DropShipping.Domain.Models;
using DropShipping.Dto.CategoryDtos;
using DropShipping.Dto.ProductDtos;

namespace DropShipping.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                ProductDtos = category.Products.Select(product => new ProductDto
                {
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity
                }).ToList()
            };
        }

        public static Category ToCategory(this AddCategoryDto addCategory)
        {
            return new Category()
            {
                Name = addCategory.Name,
                Products = addCategory.ProductDtos.Select(productDto => new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity
                }).ToList()
            };
        }
    }
}
