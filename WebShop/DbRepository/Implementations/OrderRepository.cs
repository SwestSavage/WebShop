using Microsoft.EntityFrameworkCore;
using WebShop.DbRepository.Interfaces;
using WebShop.Models;

namespace WebShop.DbRepository.Implementations
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(string connectionString, IRepositoryContextFactory repositoryContextFactory) : base(connectionString, repositoryContextFactory)
        {
        }

        public async Task ConfirmOrderByIdAsync(int orderID)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                var order = await context.Orders.
                    Include(o => o.Cart)
                     .ThenInclude(c => c.User)
                    .Include(o => o.Cart)
                    .ThenInclude(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Brand)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Type)
                    .Include(o => o.Cart)
                    .ThenInclude(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Type)
                    .Include(o => o.Cart)
                    .ThenInclude(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Color)
                    .FirstOrDefaultAsync(o => o.Id == orderID);

                order.IsConfirmed = true;

                var cart = await context.Carts
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
                    .FirstOrDefaultAsync(c => c.Id == order.Cart.Id);

                cart.ProductsFromStorage.ForEach(p =>
                {
                    if (p.Count > 0)
                    {
                        p.Count--;
                    }
                });

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            List<Order> orders = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                orders = await context.Orders
                    .Include(o => o.Cart)
                        .ThenInclude(c => c.User)
                    .Include(o => o.Cart)
                    .ThenInclude(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Brand)
                     .Include(o => o.Cart)
                     .ThenInclude(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Type)
                    .Include(o => o.Cart)
                    .ThenInclude(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Type)
                    .Include(o => o.Cart)
                    .ThenInclude(c => c.ProductsFromStorage)
                        .ThenInclude(p => p.Product)
                            .ThenInclude(p => p.Color)
                    .ToListAsync();
            }

            return orders;
        }
    }
}
