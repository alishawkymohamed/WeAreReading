using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace WeAreReading.Validators
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Please select photo");
            }
            IFormFile file = value as IFormFile;
            string extension = Path.GetExtension(file.FileName);
            if (!(file == null))
            {
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Invalid Photo, {string.Join(" ,", _extensions)} extensions are only allowed!";
        }
    }
}
