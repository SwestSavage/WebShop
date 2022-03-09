namespace WebShop.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public string Size { get; set; }
        public int Count { get; set; }
        public List<Cart> Carts { get; set; } = new();
    }
}
