using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Lab08.MVC.Business;
using Lab08.MVC.Domain;
using Lab08.MVC.Mappers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Lab08.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserProfileService userProfileService;
        private readonly IBookService bookService;
        private readonly IAuthenticationManager authenticationManager;
        private readonly UserService userService;
        private readonly IBookModelMapper bookModelMapper;
        private readonly IUserModelMapper userModelMapper;

        public UserController(
            IUserProfileService userProfileService,
            IBookService bookService,
            IAuthenticationManager authenticationManager,
            UserService userService,
            IBookModelMapper bookModelMapper,
            IUserModelMapper userModelMapper)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
            this.userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            this.authenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(authenticationManager));
            this.bookModelMapper = bookModelMapper ?? throw new ArgumentNullException(nameof(bookModelMapper));
            this.userModelMapper = userModelMapper ?? throw new ArgumentNullException(nameof(userModelMapper));
        }

        [HttpGet]
        public ActionResult Index()
        {
            UserProfile user = userProfileService.GetUserById(User.Identity.GetUserId());
            UserInfoModel userInfoModel = userModelMapper.GetUserInfoModel(user);

            ViewBag.Role = userProfileService.GetUserTypeName(user);

            if (ViewBag.Role == "Admin")
            {
                IList<BookInfoModel> allLibraryBooks = new List<BookInfoModel>();

                foreach (var book in bookService.GetAllBooks())
                {
                    allLibraryBooks.Add(bookModelMapper.GetBookInfoModel(book));
                }

                userInfoModel.Books = allLibraryBooks;
            }

            return View(userInfoModel);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginModel model, string returnUrl)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (ModelState.IsValid)
            {
                IdentityUserModel user = await userService.FindAsync(model.Login, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError(String.Empty, "Invalid login or password.");
                }
                else
                {
                    await LogInUser(user);

                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "user");
                    }

                    return Redirect(returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;

            return View(model);
        }

        [HttpGet]
        public ActionResult Logout()
        {
            authenticationManager.SignOut();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegisterModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (ModelState.IsValid)
            {
                IdentityUserModel user = new IdentityUserModel { UserName = model.Login, Email = model.Email };
                IdentityResult result = await userService.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    userProfileService.AddUser(new UserProfile { Login = model.Login, Id = user.Id });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            UserProfile userProfile = userProfileService.GetUserById(User.Identity.GetUserId());

            bookService.ReturnBooksOfUser(userProfile);
            userProfileService.DeleteUser(userProfile);

            await userService.DeleteAsync(await userService.FindByNameAsync(User.Identity.Name));

            return RedirectToAction("Logout", "User");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            UserProfile user = userProfileService.GetUserById(User.Identity.GetUserId());

            if (user == null)
            {
                throw new ErrorException("Some problems with account editing. Please, try later");
            }

            UserEditModel model = new UserEditModel { Login = user.Login, FirstName = user.FirstName, LastName = user.LastName };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserEditModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            IdentityUserModel user = await userService.FindByNameAsync(User.Identity.Name);
            UserProfile userProfile = userProfileService.GetUserById(User.Identity.GetUserId());

            if (userProfile == null || user == null)
            {
                throw new ErrorException("User is not exists");
            }

            if (ModelState.IsValid)
            {
                user.UserName = model.Login;
                userProfile.Login = model.Login;
                userProfile.FirstName = model.FirstName;
                userProfile.LastName = model.LastName;

                IdentityResult result = await userService.UpdateAsync(user);

                userProfileService.UpdateUser(userProfile);

                if (result.Succeeded)
                {
                    await LogInUser(user);

                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Something went wrong. Please, try later");
                }
            }

            return View(model);
        }

        [NonAction]
        private async Task LogInUser(IdentityUserModel user)
        {
            ClaimsIdentity claim = await userService.CreateIdentityAsync(
                user,
                DefaultAuthenticationTypes.ApplicationCookie);

            authenticationManager.SignOut();

            authenticationManager.SignIn(
                new AuthenticationProperties
                {
                    IsPersistent = true
                },
                claim);
        }
    }
}