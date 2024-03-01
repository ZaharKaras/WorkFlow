using Microsoft.Extensions.DependencyInjection;
using RSS.Core.Interfaces;
using RSS.Infastructure.Repositories;

namespace RSS.Infastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddScoped<IFeedRepository, FeedRepository>();
			services.AddScoped<IFeedUserRepository, FeedUserRepository>();

			return services;
		}
	}
}
