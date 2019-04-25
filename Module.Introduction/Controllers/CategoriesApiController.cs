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
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesApiController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: api/Api
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            return await _categoriesService.GetAllAsync();
        }

        // GET: api/Api/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categories>> GetCategories(int id)
        {
            var categories = await _categoriesService.GetAsync(id);

            if (categories == null)
            {
                return NotFound();
            }

            return categories;
        }

        // POST: api/Api
        [Route("")]
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories(Categories categories)
        {
            _categoriesService.AddAsync(categories);

            return CreatedAtAction("GetCategories", new { id = categories.CategoryId }, categories);
        }

        // DELETE: api/Api/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categories>> DeleteCategories(int id)
        {
            var categories = await _categoriesService.GetAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            await _categoriesService.DeleteAsync(categories);

            return categories;
        }

        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var ms = await _categoriesService.GetImage(id);
            return File(ms.ToArray(), "image/jpeg");
        }

        [Route("image")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, IFormFile file)
        {
            try
            {
                var categories = await _categoriesService.GetAsync(id);

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    categories.Picture = memoryStream.ToArray();
                }

                await _categoriesService.UpdateAsync(categories);

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }

        private bool CategoriesExists(int id)
        {
            return _categoriesService.GetAllAsync().Result.Any(e => e.CategoryId == id);
        }
    }
}