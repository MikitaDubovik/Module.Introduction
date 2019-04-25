using System.Collections.Generic;
using System.Threading.Tasks;
using Module.Introduction.Models;

namespace Module.Introduction.Services
{
    public interface IProductsService
    {
        Task<List<Products>> GetAllAsync();

        Task<Products> GetDetailsAsync(int id);

        Task AddAsync(Products categories);

        Task UpdateAsync(Products categories);

        Task DeleteAsync(Products categories);

        Task<Products> GetAsync(int id);

        Task<List<Products>> GetAllAsync(int numberOfProducts);
    }
}