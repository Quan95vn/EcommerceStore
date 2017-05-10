using Store.Service;
using Store.Web.Infrastructure.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Store.Web.API
{
    [RoutePrefix("api/statistic")]
    [Authorize]
    public class StatisticController : ApiControllerBase
    {
        private readonly IStatisticService _statisticService;
        public StatisticController(IErrorService errorService, IStatisticService statisticService) : base(errorService)
        {
            _statisticService = statisticService;
        }

        [Route("getrevenue")]
        [HttpGet]
        [Authorize(Roles = "View")]
        public HttpResponseMessage GetRevenueStatistic(HttpRequestMessage request, string fromDate, string toDate)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _statisticService.GetRevenueStatistic(fromDate, toDate);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}
