using System.ComponentModel.DataAnnotations.Schema;

namespace DropShipping.Domain.Models
{
    public class OrderProduct : BaseEntity
    {
        [Column("ordersId")]
        public int OrdersId { get; set; }

        public Order Order { get; set; }


        [Column("productsId")]
        public int ProductsId { get; set; }

        public Product Product { get; set; }
    }
}
