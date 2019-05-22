using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.Introduction.Contexts;

namespace Module.Introduction.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IdentityContext _context;

        public AdministrationService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<List<IdentityUser>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
