using WebShop.Models;

namespace WebShop.DbRepository.Interfaces
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public Task ConfirmOrderByIdAsync(int orderID);
    }
}
