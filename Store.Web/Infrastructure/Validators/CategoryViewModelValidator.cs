using FluentValidation;
using Store.Web.ViewModels;

namespace Store.Web.Infrastructure.Validators
{
    public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Tên danh mục không được bỏ trống.")
                .Length(1, 256).WithMessage("Tên danh mục không được vượt quá 256 ký tự.");

            RuleFor(c => c.Alias)
                .NotEmpty().WithMessage("Alias không được bỏ trống.")
                .Length(1, 256).WithMessage("Alias không được vượt quá 256 ký tự.");

            RuleFor(c => c.MetaDescription)
                .Length(0, 256).WithMessage("MetaDescription không được vượt quá 256 ký tự.");

            RuleFor(c => c.MetaKeyword)
                  .Length(0, 256).WithMessage("MetaKeyword không được vượt quá 256 ký tự.");
        }
    }
}