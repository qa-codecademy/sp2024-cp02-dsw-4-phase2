using DropShipping.Dto.ProductDtos;

namespace DropShipping.Dto.CategoryDtos
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public List<ProductDto> ProductDtos { get; set; }
    }
}
