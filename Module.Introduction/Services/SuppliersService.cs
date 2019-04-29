using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Module.Introduction.Models;
using Module.Introduction.Services.Interfaces;

namespace Module.Introduction.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly NorthwindContext _context;

        public SuppliersService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<List<Suppliers>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }
    }
}
