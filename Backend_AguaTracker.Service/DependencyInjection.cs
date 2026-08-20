using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Backend_AguaTracker.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your service layer services here
            // For example, if you have a service class:
            // services.AddScoped<IYourService, YourServiceImplementation>();

            

            return services;
        }
    }
}
