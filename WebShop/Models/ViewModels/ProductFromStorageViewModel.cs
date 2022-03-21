namespace WebShop.Models.ViewModels
{
    public class ProductFromStorageViewModel
    {
        public int StorageId { get; set; }
        public string ProductModel { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public int TypeId { get; set; }
        public string ProductDesc { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public int Count { get; set; }
    }
}
