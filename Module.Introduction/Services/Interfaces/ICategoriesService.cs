using System.Collections.Generic;
using System.Threading.Tasks;
using Module.Introduction.Models;

namespace Module.Introduction.Services
{
    public interface ICategoriesService
    {
        Task<List<Categories>> GetAllAsync();

        Task<Categories> GetDetailsAsync(int id);

        Task AddAsync(Categories categories);

        Task UpdateAsync(Categories categories);

        Task DeleteAsync(Categories categories);
    }
}
