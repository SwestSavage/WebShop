using Microsoft.EntityFrameworkCore;
using WebShop.DbRepository.Interfaces;
using WebShop.Models;
using WebShop.Models.ViewModels;

namespace WebShop.DbRepository.Implementations
{
    public class ProductsRepository : BaseRepository, IProductsRepository
    {
        public ProductsRepository(string connectionString, IRepositoryContextFactory repositoryContextFactory) : base(connectionString, repositoryContextFactory)
        {
        }

        public IEnumerable<Product> GetAll()
        {
            IEnumerable<Product> products = new List<Product>();

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.Products.Any())
                {
                    products = context.Products
                        .Include(u => u.Color)
                        .Include(u => u.Brand)
                        .Include(u => u.Type)
                        .ToList();
                }
               
            }

            return products;
        }

        public async Task<IEnumerable<ProductFromStorageViewModel>> GetAllFromStorageAsync()
        {
            List<ProductFromStorageViewModel> result = new List<ProductFromStorageViewModel>();

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.Storage.Any())
                {
                    var storage = await context.Storage
                        .Include(s => s.Product)
                        .Include(s => s.Product.Brand)
                        .Include(s => s.Product.Color)
                        .Include(s => s.Product.Type)
                        .ToListAsync();

                    foreach (var item in storage)
                    {
                        result.Add(new ProductFromStorageViewModel
                        {
                            Product = item.Product,
                            Count = item.Count,
                            Size = item.Size
                        });
                    }
                }

                return result;
            }
        }
    }
}
