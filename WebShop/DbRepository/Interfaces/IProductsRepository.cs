using WebShop.Models;
using WebShop.Models.ViewModels;

namespace WebShop.DbRepository.Interfaces
{
    public interface IProductsRepository
    {
        public Product GetById(int id);

        public IEnumerable<Product> GetAll();

        public Task<IEnumerable<Storage>> GetAllFromStorageAsync();
    }
}
