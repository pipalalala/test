using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Lab08.MVC.Business;
using Lab08.MVC.Domain;
using Lab08.MVC.Mappers;

namespace Lab08.MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IUserProfileService userProfileService;
        private readonly IBookService bookService;
        private readonly IBookModelMapper bookModelMapper;

        public BookController(
            IUserProfileService userProfileService,
            IBookService bookService,
            IBookModelMapper bookModelMapper)
        {
            this.userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            this.bookModelMapper = bookModelMapper ?? throw new ArgumentNullException(nameof(bookModelMapper));
        }

        [HttpGet]
        public ActionResult Books()
        {
            IEnumerable<BookInfoModel> books = new List<BookInfoModel>();

            UserProfile user = userProfileService.GetUserById(User.Identity.GetUserId());

            ViewBag.Role = userProfileService.GetUserTypeName(user);

            if (ViewBag.Role == "User")
            {
                books = bookService.GetFreeBooks().Select(book => bookModelMapper.GetBookInfoModel(book));
            }
            if (ViewBag.Role == "Admin")
            {
                books = bookService.GetAllBooks().Select(book => bookModelMapper.GetBookInfoModel(book));
            }

            return View(books);
        }

        [HttpGet]
        public ActionResult AddToCollection(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            UserProfile user = userProfileService.GetUserById(User.Identity.GetUserId());

            bookService.AddBookToUserBooksCollection(user, id.Value);

            return RedirectToAction("Books", "Book");
        }

        [HttpGet]
        public ActionResult AddToLibrary()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToLibrary(BookProfile model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bookService.AddBookToLibrary(model);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult Info(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            BookProfile bookProfile = bookService.GetBookById(id.Value);

            if (bookProfile == null)
            {
                throw new ErrorException("Book not found");
            }

            BookFullInfoModel book = bookModelMapper.GetBookFullInfoModel(bookProfile);

            if (book == null)
            {
                throw new ErrorException("Book not found");
            }

            if (!bookProfile.Accessibility)
            {
                book.UserName = userProfileService.GetUserLoginById(bookProfile.UserProfileId);
            }

            UserProfile user = userProfileService.GetUserById(User.Identity.GetUserId());

            if (user == null)
            {
                throw new ErrorException("User not found");
            }

            ViewBag.Role = userProfileService.GetUserTypeName(user);

            return View("Info", book);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            BookProfile book = bookService.GetBookById(id.Value);

            if (book == null)
            {
                throw new ErrorException("Book not found");
            }

            return View("Edit", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookProfile model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BookProfile book = bookService.GetBookById(model.Id);

            if (book == null)
            {
                throw new ErrorException("Book not found");
            }

            book.Title = model.Title;
            book.CreationYear = model.CreationYear;
            book.PagesCount = model.PagesCount;
            book.Authors = model.Authors;
            book.Genres = model.Genres;

            bookService.UpdateBook(book);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult RemoveFromCollection(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            UserProfile user = userProfileService.GetUserById(User.Identity.GetUserId());

            bookService.RemoveBookFromUserBooksCollection(user, id.Value);

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult RemoveFromLibrary(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            bookService.RemoveBookFromLibrary(id.Value);

            return RedirectToAction("Index", "User");
        }
    }
}