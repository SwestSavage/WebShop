namespace WebShop.Models.ViewModels
{
    public class ProductFromStorageViewModel
    {
        public Product Product { get; set; }
        public Storage Storage { get; set; }
        public string Size { get; set; }
        public int Count { get; set; }
    }
}
