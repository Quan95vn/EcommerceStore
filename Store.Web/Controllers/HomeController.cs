using AutoMapper;
using Store.Common;
using Store.Model.Models;
using Store.Service;
using Store.Web.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Store.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IFooterService _footerService;
        private readonly IProductService _productService;
        private readonly ISlideService _slideService;

        public HomeController(ICategoryService categoryService, IFooterService footerService, IProductService productService, ISlideService slideSerivce)
        {
            _categoryService = categoryService;
            _footerService = footerService;
            _productService = productService;
            _slideService = slideSerivce;
        }

        public ActionResult Index()
        {
            var featureProduct = _productService.GetFeatureProduct(6);
            var featureProductVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(featureProduct);

            var homeVm = new HomeViewModel
            {
                FeatureProductVm = featureProductVm
            };

            return View(homeVm);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Slide()
        {
            var slideModel = _slideService.GetLatestSlide(3);
            var slideVm = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);

            return View(slideVm);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Menu()
        {
            var categoryModel = _categoryService.GetAll();
            var categoryVm = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(categoryModel);
            return PartialView(categoryVm);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            return PartialView();
        }
    }
}