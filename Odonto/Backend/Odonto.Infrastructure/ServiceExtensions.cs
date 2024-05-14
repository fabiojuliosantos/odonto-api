using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Odonto.Infrastructure.Context;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Odonto.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void ConfigureInfraApp(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            

        }
    }
}
