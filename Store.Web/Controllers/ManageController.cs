using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Store.Common;
using Store.Data;
using Store.Model.Models;
using Store.Service;
using Store.Web.App_Start;
using Store.Web.Infrastructure.Core;
using Store.Web.Infrastructure.Extension;
using Store.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Store.Web.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private IOrderService _orderService;
        private IOrderDetailService _orderDetailService;
        private IProductService _productService;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService)
        {
            var provider = new DpapiDataProtectionProvider("Asp.Net Identity");
            userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                provider.Create("EmailConfirmation"));

            UserManager = userManager;
            SignInManager = signInManager;

            UserManager.UserValidator = new UserValidator<ApplicationUser>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _productService = productService;
        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {

                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        public ActionResult Index(int page = 1)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.UserInfo = UserManager.FindById(userId);

            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var order = _orderService.GetOrderByUserId(userId, page, pageSize, out totalRow);

            var oderVm = Mapper.Map<IEnumerable<OrderViewModel>>(order);


            int totalPage = (int)Math.Ceiling((double)order.Count() / pageSize);

            var paginationSet = new PaginationSet<OrderViewModel>()
            {
                Items = oderVm,
                MaxPage = totalPage,
                TotalCount = totalRow,
                TotalPage = totalPage,
                Page = page
            };

            return View(paginationSet);
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                return Redirect("/account/index");
            }
            AddErrors(result);

            return View(model);
        }

        // Addresses
        [HttpGet]
        public async Task<ActionResult> ChangeDetail()
        {
            var userInfo = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var userInfoVm = Mapper.Map<ApplicationUserViewModel>(userInfo);

            return View(userInfoVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeDetail(ApplicationUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userInDb = await UserManager.FindByIdAsync(model.Id);
            userInDb.UpdateUser(model);
            var result = await UserManager.UpdateAsync(userInDb);

            return Redirect("/tai-khoan.html");
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }


    }
}