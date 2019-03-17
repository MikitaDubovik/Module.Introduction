using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Module.Introduction.Models;

namespace Module.Introduction.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly NorthwindContext _context;
        private readonly IFilesService _filesService;

        public CategoriesService(NorthwindContext context, IFilesService filesService)
        {
            _context = context;
            _filesService = filesService;
        }

        public async Task<List<Categories>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Categories> GetDetailsAsync(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
        }

        public async Task AddAsync(Categories categories)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Categories categories)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Categories categories)
        {
            throw new NotImplementedException();
        }

        public async Task<MemoryStream> GetImage(int id)
        {
            if (Directory.Exists(@"D:\Work\CachedImages"))
            {
                var files = Directory.GetFiles(@"D:\Work\CachedImages");
                if (files.Any(f => f.Contains($@"D:\Work\CachedImages\CachedImage{id}.txt")))
                {
                    var imageBytes = await File.ReadAllBytesAsync($@"D:\Work\CachedImages\CachedImage{id}.txt");

                    using (var ms = new MemoryStream())
                    {
                        if (id < 9)
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
                if (categories.CategoryId < 9)
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
