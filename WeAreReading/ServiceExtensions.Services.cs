using Microsoft.Extensions.DependencyInjection;
using Services.Contracts;
using Services.Implementation;

namespace WeAreReading
{
    public static partial class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenValidatorService, TokenValidatorService>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGovernmentService, GovernmentService>();
            services.AddScoped<IRequestService, RequestService>();
        }
    }
}
