using Board.Application.Services;
using Board.Application.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Board.Application
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

			return services;
		}
	}
}
