using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropShipping.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public int? UserId { get; set; }


        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
