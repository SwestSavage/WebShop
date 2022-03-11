using Microsoft.AspNetCore.Mvc;
using WebShop.DbRepository.Interfaces;
using WebShop.Extensions;
using WebShop.Models;

namespace WebShop.Controllers
{
    public class CartController : Controller
    {
        private ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public IActionResult Cart()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            var cart = _cartRepository.GetByUserId(user.Id);

            if (cart is not null)
            {
                return View(cart.ProductsFromStorage);
            }

            return BadRequest();
        }
    }
}
