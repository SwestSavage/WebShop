using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebShop.DbRepository.Interfaces;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductsRepository _productsRepository;

        public HomeController(ILogger<HomeController> logger, IProductsRepository productsRepository)
        {
            _logger = logger;
            _productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var products = _productsRepository.GetAll();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}