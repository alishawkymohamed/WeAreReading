using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WeAreReading.Validators;

namespace WebApi.ViewModels
{
    public class UploadImageViewModel
    {
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
    }
}
