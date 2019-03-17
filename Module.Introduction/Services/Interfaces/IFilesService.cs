using System.IO;
using System.Threading.Tasks;
using Module.Introduction.Models;

namespace Module.Introduction.Services
{
    public interface IFilesService
    {
        Task WriteAsync(Categories categories);

        MemoryStream Read(int id);
    }
}
