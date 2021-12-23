using System.Net.Http;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using odev3.API.Cache;
using odev3.Models.Product;
using odev3.Models.User;
using odev3.Service.Product;
using odev3.Service.User;

namespace odev5.MVC.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly IUserCache userCache;

        public AccountController(IUserService _userService, IProductService _productService, IMapper _mapper, IUserCache _userCache)
        {
            userService = _userService;
            productService = _productService;
            mapper = _mapper;
            userCache = _userCache;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]


        public IActionResult Index()
        {
            return View();
        }
        [Route("account")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {//login kontrolü
            LoginUserModel user = new LoginUserModel();
            user.Email = username;
            user.Password = password;
            var userlist = userService.Get().userList;
            var productlist = productService.Get().productList;

//login işlemi geçerli sonuç verirse veriler listelenir
            if (userService.Login(user))
            {
                ViewBag.userList = userlist;
                ViewBag.productList = productlist;
                HttpContext.Session.SetString("username", username);
                userCache.Cache(user);
                return View("Success");
            }
            else
            {//geçersiz sonuçta uyarı döner
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }
        [Route("users")]
        [HttpPost]
        public IActionResult InsertUser(CreateUserModel newUser)
        {
            var data = mapper.Map<odev3.DB.Models.User>(newUser);
            var userlist = userService.Get().userList;
            var productlist = productService.Get().productList;
            ViewBag.userList = userlist;
            ViewBag.productList = productlist;
            var user = userCache.GetCachedUser();
            if (userService.Insert(data))
            {// if içinde servis çağırılıp işlem sonucu geri alınır, işlem sonucuna göre uyarı dönülür
                ViewBag.info = "Insert Success";
            }
            else
            {
                ViewBag.info = "Insert Failed";
            }
            return View("Success");
        }
        [Route("products")]
        [HttpPost]
        public IActionResult InsertProduct(CreateProductModel newProduct)
        {
            var data = mapper.Map<odev3.DB.Models.Product>(newProduct);
            var userlist = userService.Get().userList;
            var productlist = productService.Get().productList;
            ViewBag.userList = userlist;
            ViewBag.productList = productlist;
            if (productService.Insert(data))
            {
                ViewBag.infoProduct = "Insert Success";
            }
            else
            {
                ViewBag.infoProduct = "Insert Failed";
            }
            return View("Success");
        }

        [HttpPost]
        [Route("deleteuser")]
        public IActionResult DeleteUser(int id)
        {
            var userlist = userService.Get().userList;
            var productlist = productService.Get().productList;
            ViewBag.userList = userlist;
            ViewBag.productList = productlist;

            if (userService.Delete(id))
            {
                ViewBag.isDeleted = "Deleted";
                return View("Success");
            }
            else
            {
                ViewBag.isDeleted = "Delete failed.";
                return View("Success");
            }
        }
        [HttpPost]
        [Route("deleteproduct")]
        public IActionResult DeleteProduct(int id)
        {
            var userlist = userService.Get().userList;
            var productlist = productService.Get().productList;
            ViewBag.userList = userlist;
            ViewBag.productList = productlist;

            if (productService.Delete(id))
            {
                ViewBag.isDeleted = "Deleted";
                return View("Success");
            }
            else
            {
                ViewBag.isDeleted = "Delete failed.";
                return View("Success");
            }
        }

        [Route("account")]
        [HttpGet]
        public IActionResult Logout()
        {// çıkış işlemi
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

    }

}
