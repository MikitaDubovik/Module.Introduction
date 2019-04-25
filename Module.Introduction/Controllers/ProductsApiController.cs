using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Module.Introduction.Models;
using Module.Introduction.Services;

namespace Module.Introduction.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsApiController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        // GET: api/Products1
        [Route("products")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await _productsService.GetAllAsync();
        }

        // GET: api/Products1/5
        [HttpGet("products/{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _productsService.GetAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // POST: api/Products1
        [Route("products")]
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            await _productsService.AddAsync(products);

            return CreatedAtAction("GetProducts", new { id = products.ProductId }, products);
        }

        // DELETE: api/Products1/5
        [HttpDelete("products/{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var products = await _productsService.GetAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            await _productsService.DeleteAsync(products);

            return products;
        }
    }
}