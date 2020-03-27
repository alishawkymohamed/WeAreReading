﻿using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}
