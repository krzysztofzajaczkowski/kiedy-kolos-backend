using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using KiedyKolos.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace KiedyKolos.Infrastructure.IoC
{
    public static class InfrastructureContainer 
    {
      public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
      {
        services.AddDbContext<AppDbContext>(options =>
        {
          options.UseInMemoryDatabase("db");
        });

        return services;
      }
    }
}