using System.Collections.Generic;
using KiedyKolos.Core.Result;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using KiedyKolos.Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KiedyKolos.Infrastructure.Data.Repositories;

namespace KiedyKolos.Infrastructure.IoC
{
	public static class InfrastructureContainer
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
		{
            services.AddScoped<IYearCourseRepository, YearCourseRepository>();
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseInMemoryDatabase("db");
			});

            services.AddMediatR(typeof(Result));

            return services;
		}
	}
}	