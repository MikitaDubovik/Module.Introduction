using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Module.Introduction.Services
{
    public interface IAdministrationService
    {
        Task<List<IdentityUser>> GetUsersAsync();
    }
}
