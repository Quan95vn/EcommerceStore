using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Store.Common;
using Store.Model.Models;
using Store.Service;
using Store.Web.App_Start;
using Store.Web.Infrastructure.Extension;
using Store.Web.Infrastructure.Validators;
using Store.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
//using static Store.Web.App_Start.IdentityConfig;

namespace Store.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ApplicationUserManager _userManager;

        public ShoppingCartController(IProductService productService, ApplicationUserManager userManager, IOrderService orderService)
        {
            _productService = productService;
            _userManager = userManager;
            _orderService = orderService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();

            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            return Json(new
            {
                data = cart,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            if (_productService.GetById(productId).Quantity == 0)
            {
                return Json(new
                {
                    status = false
                });
            }

            if (cart == null)
                cart = new List<ShoppingCartViewModel>();

            if (cart.Any(x => x.ProductId == productId))
            {
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].ProductId == productId)
                        cart[i].Quantity += 1;
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;
                var product = _productService.GetById(productId);
                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;
                newItem.Price = (product.PromotionPrice.HasValue) ? product.PromotionPrice.Value : product.Price;
                cart.Add(newItem);
            }

            Session[CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true,
                data = cart
            });
        }

        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);

            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    if (item.ProductId == jitem.ProductId)
                    {
                        item.Quantity = jitem.Quantity;
                    }
                }
            }

            Session[CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();

            return Json(new
            {
                status = true
            });
        }

        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var user = _userManager.FindById(User.Identity.GetUserId());
                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });

        }

        [HttpGet]
        public ActionResult CheckOut()
        {

            if (Session[CommonConstants.SessionCart] == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();

            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

          

            if ((cart.Count == 0 && Session[CommonConstants.SessionCart] == null) || (cart.Count == 0 || Session[CommonConstants.SessionCart] == null))
            {
                return Redirect("/");
            }

            ViewBag.Cart = cart;

            return View(new OrderViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel orderVm)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            var varlidator = new OrderViewModelValidator();
            var result = varlidator.Validate(orderVm);
            if (!result.IsValid)
            {
                foreach (ValidationFailure failer in result.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }

                ViewBag.Cart = cart;

                return View(orderVm);
            }

            var newOrder = new Order();
            newOrder.Status = true;
            newOrder.PaymentStatus = "Đơn hàng mới";

            newOrder.UpdateOrder(orderVm);
            if (Request.IsAuthenticated)
            {
                newOrder.CustomerId = User.Identity.GetUserId();
                newOrder.CreatedBy = User.Identity.GetUserName();
            }


            var listOrderDetail = new List<OrderDetail>();

            bool isEnough = true;

            for (int i = 0; i < cart.Count; i++)
            {
                var orderDetail = new OrderDetail();
                orderDetail.ProductId = cart[i].Product.Id;
                orderDetail.Quantity = cart[i].Quantity;
                orderDetail.Price = cart[i].Product.Price;

                listOrderDetail.Add(orderDetail);
                isEnough = _productService.SellProduct(cart[i].Product.Id, cart[i].Quantity);
            }

            if (!isEnough)
            {
                return Json(new
                {
                    message = "Sản phẩm hiện đang hết hàng.",
                });
            }

            _orderService.Create(newOrder, listOrderDetail);
            _orderService.Save();

           
            return Redirect("/thank-you");
        }
    }
}