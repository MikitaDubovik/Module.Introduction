using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module.Introduction.Models;
using Module.Introduction.Services;

namespace Module.Introduction.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly NorthwindContext _context;

        private readonly ICategoriesService _categoriesService;

        public ApiController(NorthwindContext context, ICategoriesService categoriesService)
        {
            _context = context;
            _categoriesService = categoriesService;
        }

        // GET: api/Api
        [Route("categories")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Api/5
        [HttpGet("categories/{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            var categories = await _context.Categories.FindAsync(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        // PUT: api/Api/5
        [HttpPut("categories/{id}")]
        public async Task<IActionResult> PutCategories(int id, Categories categories)
        {
            if (id != categories.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(categories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Api
        [Route("categories")]
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories(Categories categories)
        {
            _context.Categories.Add(categories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = categories.CategoryId }, categories);
        }

        // DELETE: api/Api/5
        [HttpDelete("categories/{id}")]
        public async Task<ActionResult<Categories>> DeleteCategories(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();

            return categories;
        }

        [HttpGet("categories/image/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var ms = await _categoriesService.GetImage(id);
            return File(ms.ToArray(), "image/jpeg");
        }

        [Route("categories/image")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, IFormFile file)
        {
            try
            {
                var categories = _context.Categories.First(cat => cat.CategoryId == id);

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    categories.Picture = memoryStream.ToArray();
                }

                _context.Update(categories);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        // GET: api/Products1
        [Route("products")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products1/5
        [HttpGet("products/{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products1/5
        [HttpPut("products/{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products1
        [Route("products")]
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        }

        // DELETE: api/Products1/5
        [HttpDelete("products/{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
