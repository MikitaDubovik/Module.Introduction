using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Introduction.Filters;
using Module.Introduction.Models;
using Module.Introduction.Services;

namespace Module.Introduction.Controllers
{
    [TypeFilter(typeof(LoggerFilter))]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _categoriesService.GetAllAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoriesService.GetDetailsAsync(id.Value);

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description,Picture")] Categories categories, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    categories.Picture = memoryStream.ToArray();
                }

                await _categoriesService.AddAsync(categories);

                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoriesService.GetAsync(id.Value);
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,Picture")] Categories categories, IFormFile file)
        {
            if (!CategoriesExists(categories.CategoryId))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    categories.Picture = memoryStream.ToArray();
                }

                await _categoriesService.UpdateAsync(categories);

                return RedirectToAction(nameof(Index));
            }
            return View(categories);
        }

        // GET: Categories/DeleteAsync/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoriesService.GetAsync(id.Value);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Categories/DeleteAsync/5
        [HttpPost, ActionName("DeleteAsync")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categories = await _categoriesService.GetAsync(id);
            await _categoriesService.DeleteAsync(categories);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 60)]
        [HttpGet, ActionName("GetImage")]
        [Route("[controller]/[action]/{id}")]
        [Route("[controller]/image/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            return View(id);
        }

        [HttpGet, ActionName("GetFile")]
        public async Task<IActionResult> GetFile(int id)
        {
            var ms = await _categoriesService.GetImage(id);
            return File(ms.ToArray(), "image/jpeg");
        }

        private bool CategoriesExists(int id)
        {
            return _categoriesService.GetAsync(id) != null;
        }
    }
}
