using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropShipping.Domain.Models
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public int? UserId { get; set; }

        //public virtual User User { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        //public int ProductId { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
