using WebShop.DbRepository.Interfaces;
using WebShop.Models;

namespace WebShop.DbRepository.Implementations
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, IRepositoryContextFactory repositoryContextFactory) : base(connectionString, repositoryContextFactory)
        {
        }

        public IEnumerable<User> GetAll()
        {
            IEnumerable<User> users = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.Users.Any())
                {
                    users = context.Users.ToList();
                }
            }

            return users;
        }
    }
}
