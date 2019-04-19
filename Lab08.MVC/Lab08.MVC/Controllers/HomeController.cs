using System;
using System.Web.Mvc;
using Lab08.MVC.Business;

namespace Lab08.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;

        public HomeController(IBookService bookService)
        {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.BooksCount = bookService.GetBooksCount().ToString();

            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }
    }
}