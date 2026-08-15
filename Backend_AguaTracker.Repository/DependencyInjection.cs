using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend_AguaTracker.Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your repository services here
            // For example, if you have a DbContext:
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AguaTracker_DbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            return services;
        }
    }
}
