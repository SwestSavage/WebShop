using WebShop.Models;

namespace WebShop.DbRepository.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
    }
}
