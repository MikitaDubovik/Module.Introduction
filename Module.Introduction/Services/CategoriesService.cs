using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Module.Introduction.Models;

namespace Module.Introduction.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly NorthwindContext _context;

        public CategoriesService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<List<Categories>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Categories> GetDetailsAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public async Task AddAsync(Categories categories)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Categories categories)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Categories categories)
        {
            throw new NotImplementedException();
        }
    }
}
