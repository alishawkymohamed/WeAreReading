using Helpers.Contracts;
using Helpers.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace WeAreReading
{
    public static partial class ServiceExtensions
    {
        public static void AddHelpers(this IServiceCollection services)
        {
            services.AddScoped<ISessionService, SessionService>();
            services.AddSingleton<IEncryptionService, EncryptionService>();
            services.AddSingleton<IFileService, FileService>();
        }
    }
}
