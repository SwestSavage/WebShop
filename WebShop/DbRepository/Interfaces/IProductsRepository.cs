using WebShop.Models;

namespace WebShop.DbRepository.Interfaces
{
    public interface IProductsRepository
    {
        public IEnumerable<Product> GetAll();
    }
}
