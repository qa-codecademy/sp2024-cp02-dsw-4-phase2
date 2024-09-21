using DropShipping.Dto.ProductDtos;

namespace DropShipping.Dto.CategoryDtos
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }

        public List<ProductDto> ProductDtos { get; set; }
    }
}
