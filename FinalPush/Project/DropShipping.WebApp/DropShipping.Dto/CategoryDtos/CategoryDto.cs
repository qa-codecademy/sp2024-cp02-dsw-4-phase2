using DropShipping.Dto.ProductDtos;

namespace DropShipping.Dto.CategoryDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProductDto> ProductDtos { get; set; } = new List<ProductDto>();
    }
}
