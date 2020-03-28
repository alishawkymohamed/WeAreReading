using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Models.DTOs;
using WeAreReading.Validators;

namespace WeAreReading
{
    public static partial class ServiceExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<RegisterUserDTO>, UserRegisterValidator>();
        }
    }
}
