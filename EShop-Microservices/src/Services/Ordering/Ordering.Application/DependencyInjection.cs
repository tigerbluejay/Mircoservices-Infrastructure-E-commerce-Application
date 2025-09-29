using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add application services here, e.g., MediatR, AutoMapper, FluentValidation, etc.

            // services.AddMediatR(cfg => {
            // cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // });

            return services;
        }
    }
}
