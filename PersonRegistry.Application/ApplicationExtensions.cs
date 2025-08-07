using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonRegistry.Application.Behaviours;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Application.Repositories.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly);
            });
            services.Decorate(typeof(IRequestHandler<,>), typeof(CustomCommandHandlerDecorator<,>));





            return services;
        }
    }
}
