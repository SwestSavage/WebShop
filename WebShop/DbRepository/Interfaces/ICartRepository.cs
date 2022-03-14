using WebShop.Models;

namespace WebShop.DbRepository.Interfaces
{
    public interface ICartRepository
    {
        public Task AddToCartAsync(Storage storage, User user);

        public Cart GetByUserId(int userId);

        public Task RemoveFromCartAsync(int storageId, int userId);

        public Task AddOrderAsync(int cartId, decimal fullSum);

        public Task AddNewCartOfUser(int userId);
    }
}
