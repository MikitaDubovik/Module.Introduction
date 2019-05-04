using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Module.Introduction.Contexts;

namespace Module.Introduction.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly IdentityContext _context;

        public async Task<List<IdentityUserLogin<string>>> GetUsers()
        {
            return await _context.UserLogins.ToListAsync();
        }
    }
}
