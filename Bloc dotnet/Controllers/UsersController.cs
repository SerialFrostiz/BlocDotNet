using Bloc_dotnet.Datas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bloc_dotnet.Models;

namespace Bloc_dotnet.Controllers
{
    public class UsersController : Controller
    {

        private IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }


        // GET: UserController
        public  IActionResult Index()
        {
            List<User> listUsers = _userService.GetUsers();
            return View(listUsers);
        }

        // GET: UserController/Details/5
        public IActionResult Details(int id)
        {
            User user = _userService.GetUserById(id);
            if (user == null) return View("NotFound");
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                User newUser = _userService.AddUser(user);
                if (newUser != null) return View("Details", newUser);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            User user = _userService.GetUserById(id);
            if (user != null) return View(user);
            return View("NotFound");
        }

        // POST: UserController/Edit/5
        [HttpPost]
        public IActionResult Edit(User user)
        {
            dynamic updatedUser = _userService.UpdateUser(user);
            if (updatedUser.IdUser != null ) return View("Details", updatedUser);
            return View("NotFound");

        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {

            if (_userService.RemoveUser(id)) return RedirectToAction("Index");
            return View();
        }

        // GET : UserController/Login
        public ActionResult Login()
        {
            if (_userService.IsUser()) return RedirectToAction("IndexAdmin", "Salaries");
            return View();
        }


        // POST: UserController/Login
        [HttpPost]
        public ActionResult Login(User user)
        {
            var userCookie = _httpContextAccessor.HttpContext.Request.Cookies["Role"];
            if (userCookie != null && userCookie == "User")
            {
                return RedirectToAction("IndexAdmin", "Salaries");
            }
            if (_userService.Login(user))
            {                   
                CookieOptions options = new CookieOptions();
                options.Secure = true;
                options.Expires = DateTime.Now.AddMinutes(1);
                _httpContextAccessor.HttpContext.Response.Cookies.Append("Role", "User", options);
                return RedirectToAction("IndexAdmin","Salaries");
            }            
            return Redirect("Login");
        }

        // GET : UserController/Deconnection
        public ActionResult Logout()
        {
            var userCookie = _httpContextAccessor.HttpContext.Request.Cookies["Role"];
            if (userCookie != null && userCookie == "User")
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Delete("Role");
                return RedirectToAction("Index", "Salaries");
            }
            return RedirectToAction("Index", "Salaries");
        }
    }
}
