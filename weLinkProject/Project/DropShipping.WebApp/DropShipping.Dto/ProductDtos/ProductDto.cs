namespace DropShipping.Dto.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public double? SalePrice { get; set; }

        public bool OnSale { get; set; } = false;

        public int Quantity { get; set; }

        public int CategoryId { get; set; }
    }
}
