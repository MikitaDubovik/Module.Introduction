﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Module.Introduction.Infrastructure;
using Module.Introduction.Models;
using Module.Introduction.Services;
using Module.Introduction.Services.Interfaces;

namespace Module.Introduction.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationSettings _settings;
        private readonly IProductsService _productsService;
        private readonly ISuppliersService _suppliersService;
        private readonly ICategoriesService _categoriesService;

        public ProductsController(
            IOptions<ApplicationSettings> settingsOptions,
            IProductsService productsService,
            ISuppliersService suppliersService,
            ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _suppliersService = suppliersService;
            _categoriesService = categoriesService;
            _settings = settingsOptions.Value;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var numberOfProducts = _settings.NumberOfProducts;
            var products = await _productsService.GetAllAsync(numberOfProducts);

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productsService.GetDetailsAsync(id.Value);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoriesService.GetAllAsync();
            var supplierses = await _suppliersService.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(supplierses, "SupplierId", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (ModelState.IsValid)
            {
                await _productsService.AddAsync(products);
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoriesService.GetAllAsync();
            var suppliersesTemp = await _suppliersService.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", products.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliersesTemp, "SupplierId", "CompanyName", products.SupplierId);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productsService.GetAsync(id.Value);
            if (products == null)
            {
                return NotFound();
            }

            var categories = await _categoriesService.GetAllAsync();
            var suppliersesTemp = await _suppliersService.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", products.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliersesTemp, "SupplierId", "CompanyName", products.SupplierId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Products products)
        {
            if (_productsService.GetAsync(id) == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productsService.UpdateAsync(products);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
                return RedirectToAction(nameof(Index));
            }

            var categories = await _categoriesService.GetAllAsync();
            var suppliersesTemp = await _suppliersService.GetAllAsync();

            ViewData["CategoryId"] = new SelectList(categories, "CategoryId", "CategoryName", products.CategoryId);
            ViewData["SupplierId"] = new SelectList(suppliersesTemp, "SupplierId", "CompanyName", products.SupplierId);
            return View(products);
        }

        // GET: Products/DeleteAsync/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _productsService.GetWithRelatedEntitiesAsync(id.Value);

            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/DeleteAsync/5
        [HttpPost, ActionName("DeleteAsync")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _productsService.GetAsync(id);
            if (products != null)
            {
                await _productsService.DeleteAsync(products);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
