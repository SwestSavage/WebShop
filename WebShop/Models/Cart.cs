namespace WebShop.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public User User { get; set; }
        public List<Storage> ProductsFromStorage { get; set; } = new();
    }
}
