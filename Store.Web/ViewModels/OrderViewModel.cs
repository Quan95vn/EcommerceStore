using Store.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Store.Web.ViewModels
{
    public class OrderViewModel : IValidatableObject
    {
        public int Id { set; get; }
        public string CustomerName { set; get; }
        public string CustomerAddress { set; get; }
        public string CustomerEmail { set; get; }
        public string CustomerMobile { set; get; }
        public string CustomerMessage { set; get; }
        public string PaymentMethod { set; get; }
        public DateTime CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string PaymentStatus { set; get; }
        public bool Status { set; get; }
        public string CustomerId { set; get; }
        public virtual ApplicationUserViewModel User { set; get; }

        public virtual ICollection<OrderDetailViewModel> OrderDetails { set; get; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new OrderViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}