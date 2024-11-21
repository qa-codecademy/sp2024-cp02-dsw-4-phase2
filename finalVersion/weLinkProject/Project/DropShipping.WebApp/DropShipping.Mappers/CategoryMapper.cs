
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
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                ProductDtos = category.Products.Select(product => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Image = product.Image,
                    OnSale = product.OnSale,
                    SalePrice = product.SalePrice,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    CategoryId = product.CategoryId
                }).ToList()
            };
        }

        public static Category ToCategory(this AddCategoryDto addCategory)
        {
            return new Category()
            {
                Name = addCategory.Name,
                Description = addCategory.Description,
                ImageUrl = addCategory.ImageUrl,
                Products = addCategory.ProductDtos.Select(productDto => new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity,
                    Image = productDto.Image,
                    SalePrice = productDto.SalePrice,
                    OnSale = productDto.OnSale
                }).ToList()
            };
        }
    }
}
