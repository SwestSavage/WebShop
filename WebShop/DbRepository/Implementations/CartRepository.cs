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

                        existingCart.Count = existingCart.ProductsFromStorage.Count;
                    }
                }
                else
                {
                    Cart cart = new Cart
                    {
                        Count = 0,
                        User = context.Users.FirstOrDefault(u => u.Id == user.Id),
                        Sum = storage.Product.Price,
                    };                   

                    cart.ProductsFromStorage.Add(context.Storage
                        .Include(s => s.Product)
                        .Include(s => s.Product.Brand)
                        .Include(s => s.Product.Color)
                        .Include(s => s.Product.Type)
                        .FirstOrDefault(s => s.Id == storage.Id));

                    cart.Count = cart.ProductsFromStorage.Count;

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
                    .Include(c => c.User)
                    .Include(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Brand)
                     .Include(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Type)
                    .Include(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Type)
                    .Include(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Color)
                    .FirstOrDefault(c => c.User.Id == userId);
            }

            return cart;
        }

        public async Task RemoveFromCartAsync(int storageId, int userId)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                var cart = await context.Carts
                    .Include(c => c.ProductsFromStorage)
                    .FirstOrDefaultAsync(c => c.User.Id == userId);
                var stor = await context.Storage.FirstOrDefaultAsync(s => s.Id == storageId);

                if (cart is not null && stor is not null)
                {
                    cart.ProductsFromStorage.Remove(stor);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
