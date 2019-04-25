using System.Collections.Generic;
using System.IO;
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

        Task<MemoryStream> GetImage(int id);

        Task<Categories> GetAsync(int id);
    }
}
