using Microsoft.EntityFrameworkCore;
using WebShop.DbRepository.Interfaces;
using WebShop.Models;
using WebShop.Models.ViewModels;

namespace WebShop.DbRepository.Implementations
{
    public class StorageRepository : BaseRepository, IStorageRepository
    {
        public StorageRepository(string connectionString, IRepositoryContextFactory repositoryContextFactory) : base(connectionString, repositoryContextFactory)
        {
        }

        public Storage GetById(int id)
        {
            Storage storage = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                storage = context.Storage
                        .Include(s => s.Product)
                        .Include(s => s.Product.Brand)
                        .Include(s => s.Product.Color)
                        .Include(s => s.Product.Type)
                        .FirstOrDefault(s => s.Id == id);
            }

            return storage;
        }

        public void AddProductInStorage(ProductFromStorageViewModel model)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                context.Products.Add(new Product
                {
                    Brand = context.Brands.FirstOrDefault(b => b.Id == model.BrandId),
                    Type = context.ProductTypes.FirstOrDefault(p => p.Id == model.TypeId),
                    Color = context.ProductColors.FirstOrDefault(c => c.Id == model.ColorId),
                    Model = model.ProductModel,
                    Description = model.ProductDesc,
                    Price = model.Price
                }) ;

                context.SaveChanges();

                context.Storage.Add(new Storage
                {
                    Product = context.Products.OrderBy(p => p.Id).Last(),
                    Size = model.Size,
                    Count = model.Count
                });

                context.SaveChanges();
            }
        }
    }
}
