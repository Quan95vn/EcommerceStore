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
    [RoutePrefix("api/order")]
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IErrorService errorService, IOrderService orderService) :
            base(errorService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("getall")]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 10)
        {
            return CreateHttpResponse(request, () =>
            {
                var totalRow = 0;
                var result = _orderService.GetAllOrder();

                totalRow = result.Count();
                var query = result.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var responseData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query);


                var paginationSet = new PaginationSet<OrderViewModel>()
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
        [Route("getbyid/{id:int}")]
        [Authorize(Roles = "View")]
        public HttpResponseMessage Detail(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var getOrderDetailId = _orderService.GetById(id);

                var responseData = Mapper.Map<Order, OrderViewModel>(getOrderDetailId);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [Authorize(Roles = "Edit")]
        public HttpResponseMessage Update(HttpRequestMessage request, OrderViewModel orderVm)
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
                    var dbOrderDetails = _orderService.GetById(orderVm.Id);

                    dbOrderDetails.UpdateOrder(orderVm);

                    _orderService.Update(dbOrderDetails);
                    _orderService.Save();

                    var responseData = Mapper.Map<Order, OrderViewModel>(dbOrderDetails);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [HttpDelete]
        [Route("delete")]
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
                    var model = _orderService.Delete(id);
                    _orderService.Save();

                    var responseData = Mapper.Map<Order, OrderViewModel>(model);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [HttpDelete]
        [Route("deletemulti")]
        [Authorize(Roles = "Delete")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedListOrder)
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
                    var listOrder = new JavaScriptSerializer().Deserialize<List<int>>(checkedListOrder);

                    foreach (var item in listOrder)
                    {
                        var oldProduct = _orderService.Delete(item);
                    }
                    _orderService.Save();


                    response = request.CreateResponse(HttpStatusCode.Created, listOrder.Count);
                }

                return response;
            });
        }
    }
}
