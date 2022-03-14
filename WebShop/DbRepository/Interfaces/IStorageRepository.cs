using WebShop.Models;

namespace WebShop.DbRepository.Interfaces
{
    public interface IStorageRepository
    {
        public Storage GetById(int id);

        public void AddProductInStorage(Product product, Storage storage);
    }
}
