using Store.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Web.ViewModels
{
    public class SlideViewModel : IValidatableObject
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Image { set; get; }
        public int? DisplayOrder { set; get; }
        public bool Status { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new SlideViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}