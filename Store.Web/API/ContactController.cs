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
    [RoutePrefix("api/contact")]
    [Authorize]
    public class ContactController : ApiControllerBase
    {
        private readonly IContactDetailService _contactService;

        public ContactController(IErrorService errorService, IContactDetailService contactService) :
            base(errorService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                var totalRow = 0;
                var model = _contactService.GetAll(keyword);

                totalRow = model.Count();

                var query = model.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<ContactDetail>, IEnumerable<ContactDetailViewModel>>(query);

                var paginationSet = new PaginationSet<ContactDetailViewModel>()
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

                var model = _contactService.GetAll();



                var responseData = Mapper.Map<IEnumerable<ContactDetail>, IEnumerable<ContactDetailViewModel>>(model);

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
                var getContactDetailId = _contactService.GetById(id);

                var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(getContactDetailId);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Add")]
        public HttpResponseMessage Create(HttpRequestMessage request, ContactDetailViewModel ContactDetailVm)
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
                var newContactDetail = new ContactDetail();

                newContactDetail.UpdateContactDetail(ContactDetailVm);

                _contactService.Add(newContactDetail);

                _contactService.Save();

                var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(newContactDetail);

                response = request.CreateResponse(HttpStatusCode.Created, responseData);

            }
            return response;
        }

        [Route("update")]
        [HttpPut]
        [Authorize(Roles = "Edit")]
        public HttpResponseMessage Update(HttpRequestMessage request, ContactDetailViewModel ContactDetailVm)
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
                    var dbContactDetail = _contactService.GetById(ContactDetailVm.Id);

                    dbContactDetail.UpdateContactDetail(ContactDetailVm);

                    _contactService.Update(dbContactDetail);
                    _contactService.Save();

                    var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(dbContactDetail);
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
                    var deleteContactDetail = _contactService.Delete(id);

                    _contactService.Save();

                    var responseData = Mapper.Map<ContactDetail, ContactDetailViewModel>(deleteContactDetail);

                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [Authorize(Roles = "Delete")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedListContactDetail)
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

                    var listContactDetail = new JavaScriptSerializer().Deserialize<List<int>>(checkedListContactDetail);

                    foreach (var item in listContactDetail)
                    {
                        _contactService.Delete(item);
                    }
                    _contactService.Save();



                    response = request.CreateResponse(HttpStatusCode.OK, listContactDetail.Count);
                }
                return response;
            });
        }
    }
}
