using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RSS.Application.Services;
using RSS.Application.Services.Interfaces;

namespace RSS.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			var assembly = typeof(DependencyInjection).Assembly;

			services.AddMediatR(configuration =>
				configuration.RegisterServicesFromAssemblies(assembly));

			services.AddValidatorsFromAssembly(assembly);

			services.AddAutoMapper(assembly);

			services.AddScoped<IParseService, ParseService>();

			return services;
		}
	}
}
