using Microsoft.EntityFrameworkCore;
using WebShop.DbRepository.Interfaces;
using WebShop.Models;

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
    }
}
