using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Module.Introduction.Models;

namespace Module.Introduction.Services
{
    public class ProductsService : IProductsService
    {
        private readonly NorthwindContext _context;

        public ProductsService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<List<Products>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Products> GetDetailsAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
        }

        public async Task AddAsync(Products products)
        {
            _context.Products.Add(products);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Products products)
        {
            _context.Update(products);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Products products)
        {
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
        }

        public async Task<Products> GetAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(pr => pr.ProductId == id);
        }

        public async Task<List<Products>> GetAllAsync(int numberOfProducts)
        {
            var northwindContext = numberOfProducts == 0 ?
                _context.Products.Include(p => p.Category).Include(p => p.Supplier) :
                _context.Products.Include(p => p.Category).Include(p => p.Supplier).Take(numberOfProducts);
            return await northwindContext.ToListAsync();
        }
    }
}