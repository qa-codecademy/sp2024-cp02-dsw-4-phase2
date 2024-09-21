namespace DropShipping.Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public int SortOrder { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
