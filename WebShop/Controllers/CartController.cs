using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Cart()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            ViewData["LoggedIn"] = Convert.ToString(User.Identity.Name);
            ViewBag.User = user;

            var cart = _cartRepository.GetByUserId(user.Id);

            if (cart is not null)
            {
                ViewBag.ItemsInCart = cart.ProductsFromStorage.Count();

                return View(cart.ProductsFromStorage);
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFromCart(int storageId)
        {
            User user = HttpContext.Session.GetObject<User>("user");

            if (storageId != 0)
            {
                await _cartRepository.RemoveFromCartAsync(storageId, user.Id);

                return RedirectToAction("Cart", "Cart");
            }

            return BadRequest();
        }
    }
}
