using DropShipping.Dto.ProductDtos;

namespace DropShipping.Dto.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public List<ProductDto> ProductDtos { get; set; }
    }
}
