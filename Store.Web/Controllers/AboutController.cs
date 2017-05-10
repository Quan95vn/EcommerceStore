using System.Web.Mvc;

namespace Store.Web.Controllers
{
    public class AboutController : Controller
    {
        [OutputCache(Duration = 3600)]
        public ActionResult Index()
        {
            return View();
        }
    }
}