#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShop;
using WebShop.Data;

namespace WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private const int V = 45;
        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.Where(x=>x.quantity>0).ToListAsync();
        }

        // GET: api/Products
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetProductsCount()
        {
            return await _context.Products.Where(x => x.quantity > 0).CountAsync();
        }

        [HttpGet("skip/{brojSkip}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByPage(int brojSkip)
        {
            return await _context.Products.Where(x => x.quantity > 0).Skip(brojSkip * 8).Take(8).ToListAsync(); 
        }

        //GET: api/Products/search
        [HttpGet("search/{keyword}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByKeyword(string keyword)
        {
            var keywordProducts = _context.Products.Where(r => r.title.Contains(keyword));

            return Ok(keywordProducts);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute]int id, [FromBody]Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                Product edited = _context.Products.FirstOrDefault(c => c.id == id);
                edited.title = product.title;
                edited.desc = product.desc;
                edited.price = product.price;
                edited.quantity = product.quantity;
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // PUT: api/Products/5/Quantity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{Quantity}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromRoute] int Quantity, [FromBody] Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                Product edited = _context.Products.FirstOrDefault(c => c.id == id);
                edited.quantity = product.quantity - Quantity;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{catId}")]
        public async Task<ActionResult<Product>> PostProduct(int catId,Product product)
        {
            _context.Categories.Include(c => c.products).FirstOrDefault(i => i.id == catId).products.Add(product);
            //_context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }
    }
}
