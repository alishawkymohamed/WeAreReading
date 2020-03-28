using Microsoft.Extensions.DependencyInjection;

namespace WeAreReading
{
    public static partial class ServiceExtensions
    {
        public static void InjectDependancies(this IServiceCollection services) {
            services.AddRepos();
            services.AddHelpers();
            services.AddServices();
            services.AddValidators();
        }
    }
}
