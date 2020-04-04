using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Helpers.Contracts
{
    public interface IFileService
    {
        Task<string> WriteImage(IFormFile file, string path);
    }
}
