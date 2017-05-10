using System.Web.Mvc;
using System.Web.Routing;

namespace Store.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            // Forget password
            routes.MapRoute("Cảm ơn",
             "thank-you",
             new { controller = "ThankYou", action = "Index" }
         );

            // Forget password
            routes.MapRoute("Quên mật khẩu",
             "account/forgot-password",
             new { controller = "Account", action = "ForgotPassword" }
         );

            // Change password
            routes.MapRoute("Đổi mật khẩu",
               "account/password",
               new { controller = "Manage", action = "ChangePassword" }
           );

            // Change user-info
            routes.MapRoute("Đổi thông tin cá nhân",
               "account/user-info",
               new { controller = "Manage", action = "ChangeDetail" }
           );

            // Personal Info
            routes.MapRoute("Thông tin cá nhân",
               "account/index",
               new { controller = "Manage", action = "Index" }
           );

            // Login
            routes.MapRoute("Đăng nhập",
               "login",
               new { controller = "Account", action = "Login" }
           );

            // Sign-up
            routes.MapRoute("Đăng ký",
               "register",
               new { controller = "Account", action = "Register" }
           );

            // Checkout
            routes.MapRoute("Thanh toán",
               "thanh-toan",
               new { controller = "ShoppingCart", action = "CheckOut" }
           );

            // Search
            routes.MapRoute("Tìm kiếm",
               "tim-kiem",
               new { controller = "Product", action = "Search" }
           );

            // Cart
            routes.MapRoute("Giỏ hàng",
               "gio-hang",
               new { controller = "ShoppingCart", action = "Index" }
           );

            // About
            routes.MapRoute("Gioi thieu",
               "gioi-thieu",
               new { controller = "About", action = "Index" }
            );

            // Contact
            routes.MapRoute("Liên hệ",
                "lien-he",
                new { controller = "Contact", action = "Index" }
            );


            // Product Detail
            routes.MapRoute("Chi tiết sản phẩm",
                "sanpham/{id}/{alias}.html",
                new { controller = "Product", action = "Detail" },
                new { id = @"\d+" }
            );


            // Category
            routes.MapRoute(
               name: "Danh mục sản phẩm",
               url: "{alias}-nhom-{id}",
               defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                    namespaces: new string[] { "QuanShop.Web.Controllers" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Store.Web.Controllers" }
            );
        }
    }
}
