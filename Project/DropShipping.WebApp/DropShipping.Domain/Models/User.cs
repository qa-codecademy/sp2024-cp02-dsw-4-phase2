using DropShipping.Domain.Enums;

namespace DropShipping.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string CardNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();

        public Roles Role { get; set; }
    }
}
