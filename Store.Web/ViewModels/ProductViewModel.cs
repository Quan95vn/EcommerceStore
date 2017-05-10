using Store.Model.Abstract;
using Store.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Web.ViewModels
{
    public class ProductViewModel : Auditable,IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string ThumbImage { get; set; }
        public string MoreImages { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
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
        public int? CategoryId { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new ProductViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}