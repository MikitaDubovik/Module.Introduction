using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Module.Introduction.Infrastructure;
using Module.Introduction.Models;

namespace Module.Introduction.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly NorthwindContext _context;
        private readonly IFilesService _filesService;
        private readonly ApplicationSettings _applicationSettings;
        private readonly string _nameOfDirectory;

        public CategoriesService(NorthwindContext context, IFilesService filesService, IOptions<ApplicationSettings> settingsOptions)
        {
            _context = context;
            _filesService = filesService;
            _applicationSettings = settingsOptions.Value;
            _nameOfDirectory = _applicationSettings.NameOfDirectory;
        }

        public async Task<List<Categories>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Categories> GetDetailsAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public async Task AddAsync(Categories categories)
        {
            _context.Categories.Add(categories);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Categories categories)
        {
            _context.Update(categories);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Categories categories)
        {
            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();
        }

        public async Task<Categories> GetAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public async Task UpdateAsync(int id, IFormFile file)
        {
            var categories = await GetAsync(id);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                categories.Picture = memoryStream.ToArray();
            }

            await UpdateAsync(categories);
        }

        public async Task<MemoryStream> GetImage(int id)
        {
            if (Directory.Exists(_nameOfDirectory))
            {
                var files = Directory.GetFiles(_nameOfDirectory);
                if (files.Any(f => f.Contains(_nameOfDirectory + $@"\CachedImage{id}.txt")))
                {
                    var imageBytes = await File.ReadAllBytesAsync(_nameOfDirectory + $@"\CachedImage{id}.txt");

                    using (var ms = new MemoryStream())
                    {
                        if (id < _applicationSettings.IdOfBrokenImage)
                        {
                            await ms.WriteAsync(imageBytes, 78, imageBytes.Length - 78);
                        }
                        else
                        {
                            await ms.WriteAsync(imageBytes, 0, imageBytes.Length);
                        }

                        return ms;
                    }
                }
                else
                {
                    return await GetFromDb(id);
                }
            }
            else
            {
                return await GetFromDb(id);
            }
        }

        private async Task<MemoryStream> GetFromDb(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            using (var ms = new MemoryStream())
            {
                if (categories.CategoryId < _applicationSettings.IdOfBrokenImage)
                {
                    await ms.WriteAsync(categories.Picture, 78, categories.Picture.Length - 78);
                }
                else
                {
                    await ms.WriteAsync(categories.Picture, 0, categories.Picture.Length);
                }

                await _filesService.WriteAsync(categories);

                return ms;
            }
        }
    }
}
