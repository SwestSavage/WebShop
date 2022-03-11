using Microsoft.EntityFrameworkCore;
using WebShop.DbRepository.Interfaces;
using WebShop.Models;

namespace WebShop.DbRepository.Implementations
{
    public class CartRepository : BaseRepository, ICartRepository
    {
        public CartRepository(string connectionString, IRepositoryContextFactory repositoryContextFactory) : base(connectionString, repositoryContextFactory)
        {
        }

        public async Task AddToCartAsync(Storage storage, User user)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.Carts.Any())
                {
                    var existingCart = context.Carts.FirstOrDefault(c => c.User.Id == user.Id);

                    if (existingCart is not null)
                    {
                        existingCart.ProductsFromStorage.Add(context.Storage
                            .Include(s => s.Product)
                            .Include(s => s.Product.Brand)
                            .Include(s => s.Product.Color)
                            .Include(s => s.Product.Type)
                            .FirstOrDefault(s => s.Id == storage.Id));
                    }
                }
                else
                {
                    Cart cart = new Cart
                    {
                        Count = 1,
                        User = context.Users.FirstOrDefault(u => u.Id == user.Id),
                        Sum = storage.Product.Price,
                    };                   

                    cart.ProductsFromStorage.Add(context.Storage
                        .Include(s => s.Product)
                        .Include(s => s.Product.Brand)
                        .Include(s => s.Product.Color)
                        .Include(s => s.Product.Type)
                        .FirstOrDefault(s => s.Id == storage.Id));

                    await context.Carts.AddAsync(cart);
                }
                                              
                await context.SaveChangesAsync();
            }
        }

        public Cart GetByUserId(int userId)
        {
            Cart cart = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                cart = context.Carts
                    .Include(c => c.ProductsFromStorage)
                    .Include(c => c.ProductsFromStorage.Select(p => p.Product))
                    .FirstOrDefault(c => c.User.Id == userId);
            }

            return cart;
        }
    }
}
