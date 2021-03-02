using System.Collections.Generic;
using KiedyKolos.Core.Result;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using KiedyKolos.Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KiedyKolos.Infrastructure.Data.Repositories;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Middleware.Behaviors;

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

            services.AddScoped<IKeyRepository, KeyRepository>();

            services.AddScoped<ISubjectRepository, SubjectRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(typeof(BaseResult));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestAuthorizationBehavior<,>));

            return services;
		}
	}
}	