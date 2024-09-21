namespace DropShipping.Domain.Models
{
    public class Order : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string CardNumber { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
