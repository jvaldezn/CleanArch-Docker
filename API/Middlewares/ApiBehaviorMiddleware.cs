using Microsoft.AspNetCore.Mvc;

namespace API.Middlewares
{
    public static class ApiBehaviorMiddleware
    {
        public static IServiceCollection ConfigureCustomApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var errorMessage = string.Join(" ", errors);

                    var response = new
                    {
                        success = false,
                        message = errorMessage,
                        data = errors,
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
