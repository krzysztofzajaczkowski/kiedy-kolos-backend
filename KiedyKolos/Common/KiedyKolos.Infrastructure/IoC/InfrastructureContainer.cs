using System;
using System.Collections.Generic;
using FluentValidation;
using KiedyKolos.Core.Result;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using KiedyKolos.Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using KiedyKolos.Infrastructure.Data.Repositories;
using KiedyKolos.Core.Interfaces;
using KiedyKolos.Core.Middleware.Behaviors;
using KiedyKolos.Core.Validators.YearCourse;
using KiedyKolos.Core.Validators.Group;

namespace KiedyKolos.Infrastructure.IoC
{
	public static class InfrastructureContainer
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
		{
            if (Environment.GetEnvironmentVariable("USE_IN_MEMORY") == "1" || Environment.GetEnvironmentVariable("USE_IN_MEMORY") == null)
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseInMemoryDatabase("db");
                });
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseMySql(config.GetConnectionString("DatabaseConnectionString"), ServerVersion.AutoDetect(config.GetConnectionString("DatabaseConnectionString")));
                });
            }
            services.AddScoped<IYearCourseRepository, YearCourseRepository>();

            services.AddScoped<IKeyRepository, KeyRepository>();

            services.AddScoped<ISubjectRepository, SubjectRepository>();

            services.AddScoped<IGroupRepository, GroupRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(typeof(BaseResult));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestAuthorizationBehavior<,>));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DomainValidationBehavior<,>));

            services.AddValidatorsFromAssemblyContaining<CreateYearCourseCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateYearCourseGroupCommandValidator>();

            return services;
		}
	}
}	