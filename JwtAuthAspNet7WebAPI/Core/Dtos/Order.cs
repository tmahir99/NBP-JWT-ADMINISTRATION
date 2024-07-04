namespace JwtAuthAspNet7WebAPI.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderedBy { get; set; }
    }
}
