using System;
using System.IO;
using System.Threading.Tasks;
using Helpers.Contracts;
using Microsoft.AspNetCore.Http;

namespace Helpers.Implementation
{
    public class FileService : IFileService
    {
        public async Task<string> WriteImage(IFormFile file, string path)
        {
            string imageId = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            string filePath = path + imageId + "." + extension;
            using (FileStream stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
            return imageId;
        }
    }
}
