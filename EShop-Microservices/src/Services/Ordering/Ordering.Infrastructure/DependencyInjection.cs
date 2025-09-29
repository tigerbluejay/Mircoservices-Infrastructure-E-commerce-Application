using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectiongString = configuration.GetConnectionString("Database");

            // Add infrastructure services here, e.g., DbContext, Repositories, External Services, etc.
            // services.AddDbContext<OrderingDbContext>(options =>
            //     options.UseSqlServer(configuration.GetConnectionString("OrderingConnection")));

            // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
