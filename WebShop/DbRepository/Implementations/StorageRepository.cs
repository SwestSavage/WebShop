using Microsoft.EntityFrameworkCore;
using WebShop.DbRepository.Interfaces;
using WebShop.Models;

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
    }
}
