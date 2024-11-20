using DropShipping.Dto.ProductDtos;

namespace DropShipping.Dto.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public List<ProductDto> ProductDtos { get; set; }
    }
}
