using Transversal.Common;
using Infrastructure.Configuration.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace API.Extensions
{
    public static class ContextsExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString(Constant.AppDbConnection) ??
                    throw new InvalidOperationException(Constant.AppDbConnection_Error), options =>
                    {
                        options.MigrationsAssembly(Constant.Migrations_Folder);
                    });
            });

            services.AddDbContext<LogDbContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString(Constant.LogDbConnection) ??
                    throw new InvalidOperationException(Constant.LogDbConnection_Error), options =>
                    {
                        options.MigrationsAssembly(Constant.Migrations_Folder);
                    });
            });

            return services;
        }
    }
}
