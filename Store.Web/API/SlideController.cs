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
    [RoutePrefix("api/slide")]
    [Authorize]
    public class SlideController : ApiControllerBase
    {
        private readonly ISlideService _slideService;

        public SlideController(IErrorService errorService, ISlideService slideService) :
            base(errorService)
        {
            _slideService = slideService;
        }

        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                var totalRow = 0;
                var model = _slideService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(query);

                var paginationSet = new PaginationSet<SlideViewModel>()
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

                var model = _slideService.GetAll();

                var responseData = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(model);

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
                var getSlideId = _slideService.GetById(id);

                var responseData = Mapper.Map<Slide, SlideViewModel>(getSlideId);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Add")]
        public HttpResponseMessage Create(HttpRequestMessage request, SlideViewModel SlideVm)
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
                var newSlide = new Slide();

                newSlide.UpdateSlide(SlideVm);

                _slideService.Add(newSlide);

                _slideService.Save();

                var responseData = Mapper.Map<Slide, SlideViewModel>(newSlide);

                response = request.CreateResponse(HttpStatusCode.Created, responseData);

            }
            return response;
        }

        [Route("update")]
        [HttpPut]
        [Authorize(Roles = "Edit")]
        public HttpResponseMessage Update(HttpRequestMessage request, SlideViewModel SlideVm)
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
                    var dbSlide = _slideService.GetById(SlideVm.Id);

                    dbSlide.UpdateSlide(SlideVm);

                    _slideService.Update(dbSlide);
                    _slideService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(dbSlide);
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
                    var deleteSlide = _slideService.Delete(id);

                    _slideService.Save();

                    var responseData = Mapper.Map<Slide, SlideViewModel>(deleteSlide);

                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Authorize(Roles = "Delete")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedListSlide)
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

                    var listSlide = new JavaScriptSerializer().Deserialize<List<int>>(checkedListSlide);

                    foreach (var item in listSlide)
                    {
                        _slideService.Delete(item);
                    }
                    _slideService.Save();



                    response = request.CreateResponse(HttpStatusCode.OK, listSlide.Count);
                }
                return response;
            });
        }
    }
}
