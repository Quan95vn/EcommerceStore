using AutoMapper;
using Store.Common;
using Store.Model.Models;
using Store.Service;
using Store.Web.Infrastructure.Core;
using Store.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Store.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public ActionResult Category(int id, int page = 1, string sort = "", string category = "")
        {
            ViewBag.SelectedSort = sort;
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;

            var productInDb = _productService.GetListProductByCategoryPaging(id, page, pageSize, sort,out totalRow);
            var productVm = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productInDb);

            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var categoryInDb = _categoryService.GetById(id);
            ViewBag.Category = Mapper.Map<Category,CategoryViewModel>(categoryInDb);

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productVm,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = totalRow,
                TotalPage = totalPage,
                Page = page
            };
            return View(paginationSet);
        }

        public ActionResult Detail(int id)
        {
            _productService.IncreaseView(id);
            _productService.Save();

            var productInDb = _productService.GetById(id);
            var productVm = Mapper.Map<Product, ProductViewModel>(productInDb);

            var categoryInDb = _categoryService.GetById((int)productInDb.CategoryId);
            ViewBag.Category = Mapper.Map<CategoryViewModel>(categoryInDb);

            var relatedProduct = _productService.GetRelatedProduct(id, 3);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<ProductViewModel>>(relatedProduct);

            // Lấy nhiều ảnh
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productVm.MoreImages);
            ViewBag.MoreImages = listImages;


            return View(productVm);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;

            ViewBag.SelectedSort = sort;

            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                TotalCount = totalRow,
                TotalPage = totalPage,
                Page = page
            };
            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);
            return Json(new
            {
                data = model
                
            }, JsonRequestBehavior.AllowGet);
        }
    }
}