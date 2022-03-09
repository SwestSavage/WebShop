namespace WebShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Count { get; set; }
        public decimal FullSum { get; set; }
        public Cart Cart { get; set; }
    }
}
