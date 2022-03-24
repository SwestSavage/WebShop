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
        private readonly IWebHostEnvironment _appEnvironment;

        public AdminController(IProductsRepository productsRepository,
            IProductInfoRepository productInfoRepository,
            IStorageRepository storageRepository,
            IWebHostEnvironment appEnvironment)
        {
            _productsRepository = productsRepository;
            _productInfoRepository = productInfoRepository;
            _storageRepository = storageRepository;
            _appEnvironment = appEnvironment;
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
        public async Task<IActionResult> AddProduct(ProductFromStorageViewModel productStorage)
        {
            string? path = null;

            if (productStorage.UploadedFile is not null)
            {
                path = "/Files/" + productStorage.UploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await productStorage.UploadedFile.CopyToAsync(fileStream);
                }
            }

            _storageRepository.AddProductInStorage(productStorage, path);

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
        public async Task<IActionResult> UpdateProduct(ProductFromStorageViewModel productStorage)
        {
            string? path = null;

            if (productStorage.UploadedFile is not null)
            {
                path = "/Files/" + productStorage.UploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await productStorage.UploadedFile.CopyToAsync(fileStream);
                }
            }

            _storageRepository.UpdateProductInStorage(productStorage, path);

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
