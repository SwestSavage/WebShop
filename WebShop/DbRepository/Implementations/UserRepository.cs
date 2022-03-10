using WebShop.DbRepository.Interfaces;
using WebShop.Models;

namespace WebShop.DbRepository.Implementations
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString, IRepositoryContextFactory repositoryContextFactory) : base(connectionString, repositoryContextFactory)
        {
        }

        public async Task AddAsync(User user)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                await context.Users.AddAsync(user);

                await context.SaveChangesAsync();
            }
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

        public User GetById(int id)
        {
            User user = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.Users.Any())
                {
                    user = context.Users.FirstOrDefault(u => u.Id == id);
                }
            }

            return user;
        }

        public User GetByLogin(string login)
        {
            User user = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.Users.Any())
                {
                    user = context.Users.FirstOrDefault(u => u.Login == login);
                }
            }

            return user;
        }
    }
}
