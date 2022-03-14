using WebShop.Models;

namespace WebShop.DbRepository.Interfaces
{
    public interface IProductInfoRepository
    {
        public IEnumerable<Brand> GetBrands();
        public IEnumerable<ProductColor> GetProductsColors();
        public IEnumerable<ProductType> GetProductTypes();

        public void AddBrand(Brand brand);
        public void RemoveBrand(Brand brand);

        public void AddColor(ProductColor color);
        public void RemoveColor(ProductColor color);

        public void AddType(ProductType type);
        public void RemoveType(ProductType type);
    }
}
