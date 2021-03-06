namespace WebShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Brand Brand { get; set; }
        public ProductType Type { get; set; }
        public ProductColor Color { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImagePath { get; set; }
    }
}
