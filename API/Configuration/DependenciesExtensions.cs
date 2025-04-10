using System.Net.Mail;
using Application.Interface;
using Application.Services;
using Infrastructure.Configuration;
using Infrastructure.Interface;
using Infrastructure.Repositories;
using Transversal.Common.Interfaces;
using Transversal.Mappings;

namespace API.Configuration
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
