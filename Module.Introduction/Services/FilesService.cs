using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Module.Introduction.Infrastructure;
using Module.Introduction.Models;

namespace Module.Introduction.Services
{
    public class FilesService : IFilesService
    {
        private readonly ApplicationSettings _applicationSettings;

        public FilesService(IOptions<ApplicationSettings> settingsOptions)
        {
            _applicationSettings = settingsOptions.Value;
        }

        public async Task WriteAsync(Categories categories)
        {
            if (!Directory.Exists(@"D:\Work\CachedImages"))
            {
                Directory.CreateDirectory(@"D:\Work\CachedImages");
                await File.WriteAllBytesAsync(
                    $@"D:\Work\CachedImages\CachedImage{categories.CategoryId}.txt",
                    categories.Picture);
            }
            else
            {
                var numberOfFiles = Directory.GetFiles(@"D:\Work\CachedImages").Length;
                if (numberOfFiles < _applicationSettings.MaximumNumberOfImage)
                {
                    await File.WriteAllBytesAsync(
                        $@"D:\Work\CachedImages\CachedImage{categories.CategoryId}.txt",
                        categories.Picture);
                }
                else
                {
                    File.Delete(Directory.GetFiles($@"D:\Work\CachedImages").First());
                    await File.WriteAllBytesAsync(
                        $@"D:\Work\CachedImages\CachedImage{categories.CategoryId}.txt",
                        categories.Picture);
                }
            }

        }

        public MemoryStream Read(int id)
        {
            if (!Directory.Exists(@"D:\Work\CachedImages")) return null;

            var files = Directory.GetFiles(@"D:\Work\CachedImages");
            if (files.Any(f => f.Contains($@"D:\Work\CachedImages\CachedImage{id}.txt")))
            {
                var imageBytes = File.ReadAllBytes($@"D:\Work\CachedImages\CachedImage{id}.txt");

                using (var ms = new MemoryStream())
                {
                    if (id < 9)
                    {
                        ms.Write(imageBytes, 78, imageBytes.Length - 78);
                    }
                    else
                    {
                        ms.Write(imageBytes, 0, imageBytes.Length);
                    }

                    return ms;
                }
            }

            return null;
        }
    }
}
