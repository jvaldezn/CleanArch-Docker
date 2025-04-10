using System.Net.Mail;
using Application.Interface;
using Application.Services;
using Infrastructure.Configuration;
using Infrastructure.Configuration.Context;
using Infrastructure.Interface;
using Infrastructure.Messaging.Publisher;
using Infrastructure.Repositories;
using Transversal.Common.Interfaces;
using Transversal.Mappings;

namespace API.Extensions
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();
            services.AddScoped<IUnitOfWork<LogDbContext>, UnitOfWork<LogDbContext>>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<IEventPublisher, EventPublisher>();

            return services;
        }
    }
}
