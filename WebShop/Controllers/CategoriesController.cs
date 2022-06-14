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
    public class CategoriesController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _context.Categories.Include(c=>c.products).ToListAsync());
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.Include(c => c.products).FirstOrDefaultAsync(i=>i.id==id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpGet("count/{categoryId}")]
        public int GetProductCountForCategory(int categoryId)
        {
            return _context.Categories.Include(c => c.products).FirstOrDefault(i => i.id == categoryId).products.Count();
        }

        [HttpGet("skip/{categoryId}/{brojSkip}")]
        public IEnumerable<Product> GetCategoryByIdAndPage(int categoryId, int brojSkip)
        {
            return _context.Categories.Include(c => c.products).FirstOrDefault(i => i.id == categoryId).products.Skip(brojSkip * 8).Take(8).ToList();
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory([FromRoute]int id, [FromBody]Category category)
        {
                
            Category edited = _context.Categories.FirstOrDefault(c => c.id == id);
            edited.image = category.image;
            edited.desc  = category.desc;
            edited.title = category.title;    
            
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            Console.WriteLine(category);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.id == id);
        }
    }
}
