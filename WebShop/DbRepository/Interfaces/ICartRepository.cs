using WebShop.Models;

namespace WebShop.DbRepository.Interfaces
{
    public interface ICartRepository
    {
        public Task AddToCartAsync(Storage storage, User user);

        public Cart GetByUserId(int userId);
    }
}
