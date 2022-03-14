using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebShop.DbRepository.Interfaces;
using WebShop.Extensions;
using WebShop.Models;
using WebShop.Models.ViewModels;

namespace WebShop.Controllers
{
    public class AdminController : Controller
    {
        private IProductsRepository _productsRepository;
        private IProductInfoRepository _productInfoRepository;
        private IStorageRepository _storageRepository;

        public AdminController(IProductsRepository productsRepository,
            IProductInfoRepository productInfoRepository)
        { 
            _productsRepository = productsRepository;
            _productInfoRepository = productInfoRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AdminPage()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            if (user.IsAdmin)
            {
                ViewData["LoggedIn"] = Convert.ToString(User.Identity.Name);
                ViewBag.IsAdmin = true;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var products = await _productsRepository.GetAllFromStorageAsync();

            return View(products);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult AddProduct(ProductFromStorageViewModel productStorage)
        {
            if (ModelState.IsValid)
            {
                _storageRepository.AddProductInStorage(productStorage.Product, productStorage.Storage);

                return RedirectToAction("AdminPage", "Admin");
            }

            ModelState.AddModelError("", "Некорректные данные");

            return View(productStorage);
        }
    }
}
