using System.Collections.Generic;
using System.Threading.Tasks;
using Module.Introduction.Models;

namespace Module.Introduction.Services.Interfaces
{
    public interface ISuppliersService
    {
        Task<List<Suppliers>> GetAllAsync();
    }
}
