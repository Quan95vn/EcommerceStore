using FluentValidation;
using Store.Web.ViewModels;

namespace Store.Web.Infrastructure.Validators
{
    public class ContactDetailViewModelValidator : AbstractValidator<ContactDetailViewModel>
    {
        public ContactDetailViewModelValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Tên liên hệ không được bỏ trống.")
               .Length(1, 100).WithMessage("Tên liên hệ không được vượt quá 100 ký tự.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email không được bỏ trống.")
                .Length(1, 50).WithMessage("Email không được vượt quá 50 ký tự.");

            RuleFor(c => c.Message)
                .Length(1, 500).WithMessage("Nội dung không được vượt quá 50 ký tự.");

            RuleFor(c => c.Address)
               .Length(1, 100).WithMessage("Địa chỉ không được vượt quá 50 ký tự.");

            RuleFor(c => c.Website)
              .NotEmpty().WithMessage("Website không được bỏ trống.")
              .Length(1, 100).WithMessage("Website không được vượt quá 100 ký tự.");
        }
    }
}