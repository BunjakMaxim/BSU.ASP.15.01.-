using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CustomAuth.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFoundPage()
        {
            return View();
        }
    }
}