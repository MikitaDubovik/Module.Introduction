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
        private readonly string _nameOfDirectory;

        public FilesService(IOptions<ApplicationSettings> settingsOptions)
        {
            _applicationSettings = settingsOptions.Value;
            _nameOfDirectory = _applicationSettings.NameOfDirectory;
        }

        public async Task WriteAsync(Categories categories)
        {
            if (!Directory.Exists(_nameOfDirectory))
            {
                Directory.CreateDirectory(_nameOfDirectory);
                await File.WriteAllBytesAsync(
                    _nameOfDirectory + $@"\CachedImage{categories.CategoryId}.txt",
                    categories.Picture);
            }
            else
            {
                var numberOfFiles = Directory.GetFiles(_nameOfDirectory).Length;
                var maximumNumberOfImage = _applicationSettings.MaximumNumberOfImage;
                if (maximumNumberOfImage < 1)
                {
                    maximumNumberOfImage = 2;
                }

                if (numberOfFiles < maximumNumberOfImage)
                {
                    await File.WriteAllBytesAsync(
                        _nameOfDirectory + $@"\CachedImage{categories.CategoryId}.txt",
                        categories.Picture);
                }
                else
                {
                    File.Delete(Directory.GetFiles(_nameOfDirectory).First());
                    await File.WriteAllBytesAsync(
                        _nameOfDirectory + $@"\CachedImage{categories.CategoryId}.txt",
                        categories.Picture);
                }
            }

        }

        public MemoryStream Read(int id)
        {
            if (!Directory.Exists(_nameOfDirectory)) return null;

            var files = Directory.GetFiles(_nameOfDirectory);
            if (files.Any(f => f.Contains(_nameOfDirectory + $@"\CachedImage{id}.txt")))
            {
                var imageBytes = File.ReadAllBytes(_nameOfDirectory + $@"\CachedImage{id}.txt");

                using (var ms = new MemoryStream())
                {
                    if (id < _applicationSettings.IdOfBrokenImage)
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
