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

        public async Task<IEnumerable<Storage>> GetAllFromStorageAsync()
        {
            List<Storage> result = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.Storage.Any())
                {
                    result = await context.Storage
                        .Include(s => s.Product)
                        .Include(s => s.Product.Brand)
                        .Include(s => s.Product.Color)
                        .Include(s => s.Product.Type)
                        .ToListAsync();
                }

                return result;
            }
        }

        public Product GetById(int id)
        {
            Product result = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                result = context.Products.FirstOrDefault(p => p.Id == id);
            }

            return result;
        }
    }
}
