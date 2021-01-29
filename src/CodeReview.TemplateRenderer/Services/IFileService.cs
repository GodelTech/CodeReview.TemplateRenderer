using System.IO;
using System.Threading.Tasks;

namespace ReviewItEasy.TemplateRenderer.Services
{
    public interface IFileService
    {
        Task<string> ReadAllTextAsync(string path);
        Task WriteAllTextAsync(string path, string text);
        FileStream Create(string path);
        FileStream OpenRead(string path);
        bool Exists(string path);
        void Delete(string path);
    }
}