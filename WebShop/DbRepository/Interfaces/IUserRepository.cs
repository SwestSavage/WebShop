using WebShop.Models;

namespace WebShop.DbRepository.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();

        public Task AddAsync(User user);
        public User GetById(int id);
        public User GetByLogin(string login);
    }
}
