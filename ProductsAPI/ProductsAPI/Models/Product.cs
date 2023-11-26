namespace ProductsAPI.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!; // null olamaz demek
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}