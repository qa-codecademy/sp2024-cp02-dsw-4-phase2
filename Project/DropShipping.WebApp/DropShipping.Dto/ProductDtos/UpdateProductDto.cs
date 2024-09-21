using DropShipping.Dto.CategoryDtos;

namespace DropShipping.Dto.ProductDtos
{
    public class UpdateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public CategoryDto Category { get; set; }
    }
}
