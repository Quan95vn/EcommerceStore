using FluentValidation;
using Store.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Web.Infrastructure.Validators
{
    public class SlideViewModelValidator : AbstractValidator<SlideViewModel>
    {
        public SlideViewModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Tên Slide không được bỏ trống.")
             .Length(1, 256).WithMessage("Tên slide không được vượt quá 256 ký tự.");

            RuleFor(c => c.Image)
                .NotEmpty().WithMessage("Hình ảnh không được bỏ trống.")
                .Length(1, 500).WithMessage("Hình ảnh không được vượt quá 256 ký tự.");
        }
    }
}