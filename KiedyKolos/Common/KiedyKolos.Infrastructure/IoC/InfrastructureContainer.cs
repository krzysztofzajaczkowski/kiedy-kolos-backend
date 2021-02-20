using System.Collections.Generic;
using KiedyKolos.Core.Result;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using KiedyKolos.Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KiedyKolos.Infrastructure.Data.Repositories;
using KiedyKolos.Core.Interfaces;

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
            services.AddScoped<IYearCourseRepository, YearCourseRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(typeof(BaseResult));

            return services;
		}
	}
}	