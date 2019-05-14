using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module.Introduction.Models;
using Module.Introduction.Services;

namespace Module.Introduction.Controllers
{
    [EnableCors("SiteCorsPolicy")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesApiController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesApiController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: api
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        {
            return await _categoriesService.GetAllAsync();
        }

        // GET: api/5
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

        // POST: api
        [HttpPost]
        public async Task<ActionResult<Categories>> PostCategories(Categories categories)
        {
            await _categoriesService.AddAsync(categories);

            return CreatedAtAction("GetCategories", new { id = categories.CategoryId }, categories);
        }

        // DELETE: api/5
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
                await _categoriesService.UpdateAsync(id, file);

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
        }
    }
}