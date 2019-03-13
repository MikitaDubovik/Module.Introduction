using System.Collections.Generic;
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

        public Task<List<Products>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Products> GetDetailsAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.ProductId == id);
        }

        public Task AddAsync(Products categories)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Products categories)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(Products categories)
        {
            throw new System.NotImplementedException();
        }
    }
}