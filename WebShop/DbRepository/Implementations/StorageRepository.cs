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

        public void UpdateProductInStorage(ProductFromStorageViewModel model)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                var storage = context.Storage
                    .Include(s => s.Product)
                    .Include(s => s.Product.Brand)
                    .Include(s => s.Product.Color)
                    .Include(s => s.Product.Type)
                    .FirstOrDefault(s => s.Id == model.StorageId);

                var product = context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Color)
                    .Include(p => p.Type)
                    .FirstOrDefault(p => p.Id == storage.Product.Id);

                if (product is not null)
                {
                    product.Brand = context.Brands.FirstOrDefault(b => b.Id == model.BrandId);

                    product.Color = context.ProductColors.FirstOrDefault(c => c.Id == model.ColorId);

                    product.Type = context.ProductTypes.FirstOrDefault(t => t.Id == model.TypeId);

                    product.Description = model.ProductDesc;

                    product.Model = model.ProductModel;

                    product.Price = model.Price;

                }

                if (storage is not null)
                {
                    storage.Count = model.Count;

                    if (model.Size is not null) storage.Size = model.Size;

                    storage.Product = product;
                }

                context.SaveChanges();
            }
        }

        public void DeleteProductInStorage(int id)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                var storage = context.Storage
                    .Include(s => s.Product)
                    .Include(s => s.Product.Brand)
                    .Include(s => s.Product.Color)
                    .Include(s => s.Product.Type)
                    .FirstOrDefault(s => s.Id == id);

                context.Storage.Remove(storage);

                context.SaveChanges();
            }
        }
    }
}
