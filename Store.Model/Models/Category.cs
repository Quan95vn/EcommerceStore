using Store.Model.Abstract;
using System.Collections.Generic;

namespace Store.Model.Models
{
    public class Category : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int? ParentId { get; set; }
        public int? DisplayOrder { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
