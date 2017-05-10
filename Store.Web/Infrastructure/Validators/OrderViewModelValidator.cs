using FluentValidation;
using Store.Web.ViewModels;

namespace Store.Web.Infrastructure.Validators
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(c => c.CustomerName)
             .NotEmpty().WithMessage("Hãy nhập tên của bạn.")
             .Length(0, 256).WithMessage("Họ tên không được vượt quá 256 ký tự.");

            RuleFor(c => c.CustomerAddress)
                .NotEmpty().WithMessage("Hãy nhập địa chỉ của bạn.")
                .Length(0, 256).WithMessage("Địa chỉ không được vượt quá 256 ký tự.");

            RuleFor(c => c.CustomerEmail)
                .EmailAddress().WithMessage("Định dạng email không chính xác.")
                .NotEmpty().WithMessage("Hãy nhập email của bạn.")
                .Length(0, 256).WithMessage("Email không được vượt quá 256 ký tự.");

            RuleFor(c => c.CustomerMobile)
                .NotEmpty().WithMessage("Yêu cầu nhập số điên thoại.")
                .Length(0, 50).WithMessage("Số điện thoại không được vượt quá 50 ký tự.");

            //RuleFor(o => o.PaymentMethod)
            //   .NotEmpty().WithMessage("Hãy nhập phương thức thanh toán");
        }
    }
}