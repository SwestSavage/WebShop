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
            IProductInfoRepository productInfoRepository,
            IStorageRepository storageRepository)
        { 
            _productsRepository = productsRepository;
            _productInfoRepository = productInfoRepository;
            _storageRepository = storageRepository;
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
            ViewBag.Colors = _productInfoRepository.GetProductsColors();
            ViewBag.Types = _productInfoRepository.GetProductTypes();
            ViewBag.Brands = _productInfoRepository.GetBrands();

            return View();
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult AddProduct(ProductFromStorageViewModel productStorage)
        {
            _storageRepository.AddProductInStorage(productStorage);

            return RedirectToAction("AdminPage", "Admin");
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateProduct(int storageId)
        {
            ViewBag.Colors = _productInfoRepository.GetProductsColors();
            ViewBag.Types = _productInfoRepository.GetProductTypes();
            ViewBag.Brands = _productInfoRepository.GetBrands();

            var storage = _storageRepository.GetById(storageId);

            var model = new ProductFromStorageViewModel
            {
                StorageId = storageId,
                ProductModel = storage.Product.Model,
                BrandId = storage.Product.Brand.Id,
                ColorId = storage.Product.Color.Id,
                TypeId = storage.Product.Type.Id,
                ProductDesc = storage.Product.Description,
                Price = storage.Product.Price,
                Size = storage.Size,
                Count = storage.Count
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateProduct(ProductFromStorageViewModel model)
        {
            _storageRepository.UpdateProductInStorage(model);

            return RedirectToAction("AdminPage", "Admin");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteProduct(int storageId)
        {
            _storageRepository.DeleteProductInStorage(storageId);

            return RedirectToAction("AdminPage", "Admin");
        }
    }
}
