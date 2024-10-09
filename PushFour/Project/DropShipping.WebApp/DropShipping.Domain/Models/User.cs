using DropShipping.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropShipping.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Roles Role { get; set; }

        public virtual List<Product> Products { get; set; } = new List<Product>();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
