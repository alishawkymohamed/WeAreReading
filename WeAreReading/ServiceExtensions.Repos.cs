﻿using Microsoft.Extensions.DependencyInjection;
using Repos.Contracts;
using Repos.Implementation;

namespace WeAreReading
{
    public static partial class ServiceExtensions
    {
        public static void AddRepos(this IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IUserRoleRepo, UserRoleRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IUserTokenRepo, UserTokenRepo>();
            services.AddScoped<IGovernmentRepo, GovernmentRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IBookRepo, BookRepo>();
        }
    }
}
