namespace DropShipping.Domain.Models
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
