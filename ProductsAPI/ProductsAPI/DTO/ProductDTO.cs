using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.DTO
{
    public class ProductDTO  // kullanıcının görmesini isteğimiz veriler
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!; // null olamaz demek
        public decimal Price { get; set; }
    }
}