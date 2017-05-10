using AutoMapper;
using Store.Model.Models;
using Store.Service;
using Store.Web.Infrastructure.Core;
using Store.Web.Infrastructure.Extension;
using Store.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Store.Web.API
{
    [RoutePrefix("api/category")]
    [Authorize]
    public class CategoryController : ApiControllerBase
    {
        #region
        private readonly ICategoryService _categoryService;

        public CategoryController(IErrorService errorService, ICategoryService categoryService) :
            base(errorService)
        {
            _categoryService = categoryService;
        }
        #endregion


        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                var totalRow = 0;
                var model = _categoryService.GetAll(page, pageSize, keyword);
                totalRow = model.Count();

                var query = model.OrderBy(x => x.CreatedDate)
                    .Skip(page * pageSize)
                    .Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(query);

                var paginationSet = new PaginationSet<CategoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [HttpGet]
        [Route("getallparents")]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetAllParent(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                var model = _categoryService.GetAll();



                var responseData = Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(model);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }


        [HttpGet]
        [Route("getbyid/{id:int}")]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {


            return CreateHttpResponse(request, () =>
            {
                var getCategoryId = _categoryService.GetById(id);

                var responseData = Mapper.Map<Category, CategoryViewModel>(getCategoryId);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Add")]
        public HttpResponseMessage Create(HttpRequestMessage request, CategoryViewModel CategoryVm)
        {
            HttpResponseMessage response = null;
            if (!ModelState.IsValid)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest,
                    ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                          .Select(m => m.ErrorMessage).ToArray());
            }
            else
            {
                var newCategory = new Category();

                newCategory.UpdateCategory(CategoryVm);
                newCategory.CreatedDate = DateTime.Now;

                _categoryService.Add(newCategory);

                _categoryService.Save();

                var responseData = Mapper.Map<Category, CategoryViewModel>(newCategory);

                response = request.CreateResponse(HttpStatusCode.Created, responseData);

            }
            return response;
        }

        [Route("update")]
        [HttpPut]
        [Authorize(Roles = "Edit")]
        public HttpResponseMessage Update(HttpRequestMessage request, CategoryViewModel CategoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest,
                        ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                              .Select(m => m.ErrorMessage).ToArray());
                }
                else
                {
                    var dbCategory = _categoryService.GetById(CategoryVm.Id);

                    dbCategory.UpdateCategory(CategoryVm);

                    _categoryService.Update(dbCategory);
                    _categoryService.Save();

                    var responseData = Mapper.Map<Category, CategoryViewModel>(dbCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [Authorize(Roles = "Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var deleteCategory = _categoryService.Delete(id);

                    _categoryService.Save();

                    var responseData = Mapper.Map<Category, CategoryViewModel>(deleteCategory);

                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Authorize(Roles = "Delete")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedListCategory)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    var listCategory = new JavaScriptSerializer().Deserialize<List<int>>(checkedListCategory);

                    foreach (var item in listCategory)
                    {
                        _categoryService.Delete(item);
                    }
                    _categoryService.Save();



                    response = request.CreateResponse(HttpStatusCode.OK, listCategory.Count);
                }
                return response;
            });
        }
    }
}
