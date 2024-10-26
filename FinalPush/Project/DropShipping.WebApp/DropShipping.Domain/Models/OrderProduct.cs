namespace DropShipping.Domain.Models
{
    public class OrderProduct
    {
        public int OrdersId { get; set; }

        public Order Order { get; set; }


        public int ProductsId { get; set; }

        public Product Product { get; set; }
    }
}
