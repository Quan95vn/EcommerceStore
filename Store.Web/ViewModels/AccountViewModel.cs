using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Web.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Bạn cần nhập họ tên ")]
        [StringLength(100, ErrorMessage = "Họ tên chỉ được phép từ 6-20 kí tự", MinimumLength = 6)]
        public string FullName { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập  mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu chỉ được phép từ 6-20 kí tự", MinimumLength = 6)]
        public string Password { set; get; }

        [Compare("Password", ErrorMessage = "Mật khẩu và nhập lại mật khẩu không trùng khớp")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không đúng.")]
        [StringLength(100, ErrorMessage = "Email chỉ được phép từ 6-20 kí tự", MinimumLength = 6)]
        public string Email { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập tên tài khoản ")]
        [StringLength(100, ErrorMessage = "Tên tài khoản chỉ được phép từ 6-20 kí tự", MinimumLength = 6)]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        [StringLength(50, ErrorMessage = "Số điện thoại chỉ được phép từ 6-20 kí tự", MinimumLength = 9)]
        public string PhoneNumber { set; get; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Bạn cần nhập email.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Định dạnh email chính xác")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ?")]
        public bool RememberMe { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu hiện tại.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu hiện tại")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu mới.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất 6 kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu và xác nhận phải không trùng nhau.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="Hãy nhập email hiện tại của bạn.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất 6 kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không trùng nhau.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Yêu cầu nhập Email.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

}