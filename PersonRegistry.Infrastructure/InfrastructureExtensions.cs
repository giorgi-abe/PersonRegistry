using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Application.Repositories.Aggregates;
using PersonRegistry.Infrastructure.Persistence;
using PersonRegistry.Infrastructure.Persistence.MappingProfiles;
using PersonRegistry.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static void ApplyMigrations(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<PersonRegistryDbContext>();
                    context.Database.Migrate();
                    Console.WriteLine("Database migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
                    throw;
                }
            }
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PersonRegistryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(
               typeof(PersonRegistry.Infrastructure.Persistence.MappingProfiles.PersonProfile).Assembly
           );
            services.AddSingleton<PersonAssembler>();
            services.AddScoped<IdentityMap>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPersonRepository, PersonRepository>();


           

            return services;
        }
    }
}
