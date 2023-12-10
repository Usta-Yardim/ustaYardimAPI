using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    //localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        
        private readonly ProductContext _context;

        public ProductsController(ProductContext context) {
           
            _context = context;
        } 


        //localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts(){

            var products = await _context.Products.Where(i => i.IsActive).Select(p => ProductToDTO(p)).ToListAsync();

            return  Ok(products); // products null ise kendi değer gönder
        }


        //localhost:5000/api/products/1 => GET
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id){
            
            if (id == null)
            {
                return NotFound();
            }
             //önce where ekledik ki bulamayınca hata vermesin. dto objesini değiştirdik
            var p = await _context.Products.Where(p => p.ProductId == id).Select(p => ProductToDTO(p)).FirstOrDefaultAsync();  // _products null değilse FirsoD çalışır

            if (p == null){
                return NotFound();
            }

            return  Ok(p);
        }

        [HttpPost]  // Veri Ekleme db'ye
        public async Task<IActionResult> CreateProduct(Product entity){

            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId}, entity);  // status code 201
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateProduct(int id, Product entity){

            if(id != entity.ProductId){
                return BadRequest(); // id yanlış olursa status code 400 bad request
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if (product == null){
                return NotFound();
            }

            product.ProductName = entity.ProductName;
            product.Price = entity.Price;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent(); // status code 204 güncelledim döndürecek bir şey yok
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id){

            if (id == null)
            {
                NotFound();
            }
            
            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);
            
            if (product == null){
                return NotFound();
            }
    
            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent(); 

        }

        private static ProductDTO ProductToDTO(Product p){
            
            var entity = new ProductDTO();
            
            if(p != null){
                entity.ProductId = p.ProductId;
                entity.ProductName = p.ProductName;
                entity.Price = p.Price;
            }
            return entity;
        }

    }

}