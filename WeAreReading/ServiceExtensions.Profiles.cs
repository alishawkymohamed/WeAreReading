using AutoMapper;
using Helpers.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Models.HelperModels;
using WeAreReading.Mappers;

namespace WeAreReading
{
    public static partial class ServiceExtensions
    {
        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RoleMapper());
                cfg.AddProfile(new GovernmentMapper());
                cfg.AddProfile(new CategoryMapper());
                cfg.AddProfile(new StatusMapper());
                cfg.AddProfile(new RequestMapper());
                cfg.AddProfile(new BookMapper());
                cfg.AddProfile(new UserDtoProfile());
                cfg.AddProfile(new UserRegisterProfile(provider.GetService<IEncryptionService>(), provider.GetService<IOptions<AppSettings>>()));
            }).CreateMapper());
        }
    }
}
