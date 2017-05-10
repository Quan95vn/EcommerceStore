using Store.Model.Abstract;
using System.Collections.Generic;

namespace Store.Model.Models
{
    public class Product : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }      
        public string ThumbImage { get; set; }
        public string MoreImages { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal  Price { get; set; }
        public decimal? PromotionPrice { get; set; }
        public int? Warranty { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string InfoDetail { get; set; }
        public string Tags { get; set; }
        public bool? HotFlag { get; set; }
        public bool? IsFeature { get; set; }
        public int? ViewCount { get; set; }
        public int Quantity { get; set; }

  
        public virtual Category Category { get; set; }
        public int? CategoryId { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
