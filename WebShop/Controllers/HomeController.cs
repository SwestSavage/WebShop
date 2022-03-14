using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShop.DbRepository.Interfaces;
using WebShop.Models;
using WebShop.Models.ViewModels;
using WebShop.Extensions;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductsRepository _productsRepository;
        private ICartRepository _cartRepository;
        private IStorageRepository _storageRepository;
        private IUserRepository _userRepository;

        private List<Product> _products = new();

        public HomeController(ILogger<HomeController> logger, 
            IProductsRepository productsRepository,
            ICartRepository cartRepository,
            IStorageRepository storageRepository,
            IUserRepository userRepository)
        {
            _logger = logger;
            _productsRepository = productsRepository;
            _cartRepository = cartRepository;
            _storageRepository = storageRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["LoggedIn"] = Convert.ToString(User.Identity.Name);
            User user = HttpContext.Session.GetObject<User>("user");

            if (user is not null && user.Id != 0)
            {
                Cart cart = _cartRepository.GetByUserId(user.Id);

                if (cart is not null)
                {
                    ViewBag.ItemsInCart = cart.ProductsFromStorage.Count();
                }

                if (user.IsAdmin)
                {                   
                    return RedirectToAction("AdminPage", "Admin");
                }
            }

            var products = await _productsRepository.GetAllFromStorageAsync();
            return View(products);
        }

        

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddToCart(int storageId)
        {
            var storage = _storageRepository.GetById(storageId);
            User user = HttpContext.Session.GetObject<User>("user");

            if (storage is not null)
            {              
                await _cartRepository.AddToCartAsync(storage, user);              
            }

            return RedirectToAction("Index", user);
        }

        [Authorize]
        public IActionResult GoToCart()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            return RedirectToAction("Cart", "Cart");      
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult TestUser()
        {
            return Content(User.Identity.Name);
        }

    }
}