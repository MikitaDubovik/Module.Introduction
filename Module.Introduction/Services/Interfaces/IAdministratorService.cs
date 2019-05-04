using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Module.Introduction.Services
{
    public interface IAdministratorService
    {
        Task<List<IdentityUserLogin<string>>> GetUsers();
    }
}
