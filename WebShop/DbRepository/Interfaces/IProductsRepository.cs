using WebShop.Models;
using WebShop.Models.ViewModels;

namespace WebShop.DbRepository.Interfaces
{
    public interface IProductsRepository
    {
        public IEnumerable<Product> GetAll();

        public Task<IEnumerable<ProductFromStorageViewModel>> GetAllFromStorageAsync();
    }
}
