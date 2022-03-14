using WebShop.DbRepository.Interfaces;
using WebShop.Models;

namespace WebShop.DbRepository.Implementations
{
    public class ProductInfoRepository : BaseRepository, IProductInfoRepository
    {
        public ProductInfoRepository(string connectionString, IRepositoryContextFactory repositoryContextFactory) : base(connectionString, repositoryContextFactory)
        {
        }

        public void AddBrand(Brand brand)
        {
           using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
           {
                context.Brands.Add(brand);

                context.SaveChanges();
           }
        }

        public void AddColor(ProductColor color)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                context.ProductColors.Add(color);

                context.SaveChanges();
            }
        }

        public void AddType(ProductType type)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                context.ProductTypes.Add(type);

                context.SaveChanges();
            }
        }

        public IEnumerable<Brand> GetBrands()
        {
            List<Brand> brands = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.Brands.Any())
                {
                    brands = context.Brands.ToList();
                }
            }

            return brands;
        }

        public IEnumerable<ProductColor> GetProductsColors()
        {
            List<ProductColor> productColors = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.ProductColors.Any())
                {
                    productColors = context.ProductColors.ToList();
                }
            }

            return productColors;
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            List<ProductType> productTypes = null;

            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                if (context.ProductTypes.Any())
                {
                    productTypes = context.ProductTypes.ToList();
                }
            }

            return productTypes;
        }

        public void RemoveBrand(Brand brand)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                context.Brands.Remove(context.Brands.FirstOrDefault(b => b.Id == brand.Id));

                context.SaveChanges();
            }
        }

        public void RemoveColor(ProductColor color)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                context.ProductColors.Remove(context.ProductColors.FirstOrDefault(c => c.Id == color.Id));

                context.SaveChanges();
            }
        }

        public void RemoveType(ProductType type)
        {
            using (var context = RepositoryContextFactory.CreateDbContext(ConnectionString))
            {
                context.ProductTypes.Remove(context.ProductTypes.FirstOrDefault(t => t.Id == type.Id));

                context.SaveChanges();
            }
        }
    }
}
