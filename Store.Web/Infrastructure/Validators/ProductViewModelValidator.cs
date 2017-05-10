using FluentValidation;
using Store.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Store.Web.Infrastructure.Validators
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Tên danh mục không được bỏ trống.")
               .Length(0, 256).WithMessage("Tên danh mục không được vượt quá 256 ký tự.");

            RuleFor(c => c.Alias)
                .NotEmpty().WithMessage("Alias không được bỏ trống.")
                .Length(0, 256).WithMessage("Alias danh mục không được vượt quá 256 ký tự.");

            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("Yêu cầu nhập giá sản phẩm")
                .GreaterThan(0).WithMessage("Giá sản phẩm phải lớn hơn hoặc bằng 0");

            RuleFor(c => c.PromotionPrice)
                .GreaterThan(0).WithMessage("Giá khuyến mại phải lớn hơn 0");
                 
            RuleFor(c => c.Warranty)
               .GreaterThan(0).WithMessage("Thời gian bảo hành phải lớn hơn 0");

            RuleFor(c=>c.MetaDescription)
                  .Length(0, 256).WithMessage("MetaDescription không được vượt quá 256 ký tự.");

            RuleFor(c => c.MetaKeyword)
                  .Length(0, 256).WithMessage("MetaKeyword không được vượt quá 256 ký tự.");
        }

    }
}