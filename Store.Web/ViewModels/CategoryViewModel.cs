using Store.Model.Abstract;
using Store.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Store.Web.ViewModels
{
    public class CategoryViewModel : Auditable,IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int? ParentId { get; set; }
        public int? DisplayOrder { get; set; }

        public virtual ICollection<ProductViewModel> Products { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new CategoryViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}