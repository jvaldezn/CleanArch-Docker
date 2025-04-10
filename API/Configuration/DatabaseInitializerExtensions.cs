using Infrastructure.Configuration.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Configuration
{
    public static class DatabaseInitializerExtensions
    {
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();
                logger.LogError(ex, "Error aplicando migraciones.");
            }

            return app;
        }
    }
}
