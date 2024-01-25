using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    // ApiController eklenerek controller olduğunu belirtiriz
    [ApiController]
    [Route("api/[controller]")]
    // ControllerBase eklenir çünkü controller base'den türettiğimiz için 
    public class ProductsController: ControllerBase
    {
        private readonly ProductContext _context;

        // Burada ProductContext içinde verilerimize erişim sağlıyoruz ve burada ctor'da context oluşturp yukarda ilk çağırdığımız contexte eşitliyoruz
        public ProductsController(ProductContext context)
        {
            _context = context; 
        }

        // Burada manuel olarak ekelemiştirk artık EF bağlantısı ile construtor içinde aldık
        //public ProductsController()
        //{
        //    List<Product> products = new List<Product>
        //    {
        //        new()  { ProductId = 1, ProductName = "Huaveyi", Price = 7500, IsActive = true },
        //        new()  { ProductId = 2, ProductName = "Afyon 68", Price = 18500, IsActive = true },
        //        new()  { ProductId = 3, ProductName = "General Mobaayl", Price = 2500, IsActive = true },
        //        new()  { ProductId = 4, ProductName = "Zamzung", Price = 13250, IsActive = true }
        //    };
        //    _products = products;
        //}


        // localhost:8080:/api/product => GET
        //[HttpGet]
        //public List<Product> GetProducts()
        //{
        //    // ?? işareti _products null'sa bunu yap demektir null değilse zaten _products return edilir kısaca ternatary'in daha kısası
        //    return _products ?? new List<Product>();
        //}

        // Burada async işlemlerden önceki hali
        //[HttpGet]
        //public IActionResult GetProducts()
        //{
        //    // Hata kodlarını yakalamak için kontrol yazılır
        //    if(_products == null)
        //    {
        //        // ürün yoksa 404 döndür
        //        return NotFound();
        //    }

        //    // varsa _products'ı döndürdük
        //    return Ok(_products);
        //}


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // localhost:8080:/api/product/1 => GET
        //[HttpGet("{id}")]
        //public Product GetProduct(int id)
        //{
        //    // tek bir değeri amak için FirstOrDefault kullandık ve dedik ki benim ProductId'm verilen parametre de varsa döndür 
        //    // _products null gelme ihtimaline karşın ? bunu koyduk ve yine null kontrolü yaptık ve new Product diyerek boş bir Product döndürürüz
        //    return _products?.FirstOrDefault(i => i.ProductId == id) ?? new Product();
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var p = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        // HTTP Post request
        [HttpPost]
        public async Task<IActionResult> CreateProdcut(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if( id != product.ProductId)
            {
                return BadRequest();
            }

            var p = await _context.Products.FirstOrDefaultAsync( i => i.ProductId == id);

            if(p == null)
            {
                return NotFound();
            }

            p.ProductName = product.ProductName;
            p.Price = product.Price;
            p.IsActive = product.IsActive;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null)
            {
                return NotFound(); 
            }

            var product = await _context.Products.FirstOrDefaultAsync(i => i.ProductId == id);
            
            if (product == null)
            {
                return NotFound();
            }
             _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
