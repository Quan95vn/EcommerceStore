using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Store.Web.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { set; get; }

        [Display(Name = "Họ tên : ")]
        public string FullName { set; get; }

        public DateTime? BirthDay { set; get; }

        [Display(Name = "Email : ")]
        public string Email { set; get; }

        [Display(Name = "Mật khẩu : ")]
        public string Password { set; get; }

        [Display(Name = "Tên tài khoản : ")]
        public string UserName { set; get; }

        [Display(Name = "Địa chỉ : ")]
        public string Address { get; set; }

        [Display(Name = "Số điện thoại : ")]
        public string PhoneNumber { set; get; }

        public IEnumerable<ApplicationGroupViewModel> Groups { set; get; }
    }
}