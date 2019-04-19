using System.Web.Mvc;

namespace Lab08.MVC.Controllers
{
    public class ErrorsController : Controller
    {
        [HttpGet]
        public ActionResult NotFound()
        {
            return View();
        }
    }
}