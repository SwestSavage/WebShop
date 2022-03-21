using WebShop.Models;
using WebShop.Models.ViewModels;

namespace WebShop.DbRepository.Interfaces
{
    public interface IStorageRepository
    {
        public Storage GetById(int id);

        public void AddProductInStorage(ProductFromStorageViewModel model);

        public void UpdateProductInStorage(ProductFromStorageViewModel model);

        public void DeleteProductInStorage(int id);
    }
}
