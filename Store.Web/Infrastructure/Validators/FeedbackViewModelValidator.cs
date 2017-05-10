using FluentValidation;
using Store.Web.ViewModels;

namespace Store.Web.Infrastructure.Validators
{
    public class FeedbackViewModelValidator : AbstractValidator<FeedbackViewModel>
    {
        public FeedbackViewModelValidator()
        {
            RuleFor(c => c.Name)
             .NotEmpty().WithMessage("Hãy nhập họ tên của bạn.")
             .Length(1, 100).WithMessage("Họ tên không được vượt quá 100 ký tự.");

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("Định dạng Email không chính xác.")
                .NotEmpty().WithMessage("Hãy điền Email của bạn.")
                .Length(1, 100).WithMessage("Email không được vượt quá 100 ký tự.");

            RuleFor(c => c.Message)
            .NotEmpty().WithMessage("Hãy nhập nôi dung bạn cần gửi.")
            .Length(1, 500).WithMessage("Nội dung không được vượt quá 500 ký tự.");

        }
    }
}