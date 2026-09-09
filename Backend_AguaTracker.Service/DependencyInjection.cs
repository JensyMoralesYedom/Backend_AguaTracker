using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Backend_AguaTracker.Service.Interfaces;
using Backend_AguaTracker.Service.Services;

namespace Backend_AguaTracker.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your service layer services here
            // For example, if you have a service class:
            // services.AddScoped<IYourService, YourServiceImplementation>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWaterIntakeService, WaterIntakeService>();
            services.AddScoped<IDailyResumeService, DailyResumeService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
