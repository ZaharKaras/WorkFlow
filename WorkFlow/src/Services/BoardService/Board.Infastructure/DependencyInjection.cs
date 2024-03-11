using Board.Core.Interfaces;
using Board.Infrastructure.Email;
using Board.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Board.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddScoped<ICardRepository, CardRepository>();
			services.AddScoped<IBoardRepository, BoardRepository>();
			services.AddScoped<IBoardUserRepository, BoardUserRepository>();
			services.AddScoped<ICardUserRepository, CardUserRepository>();
			services.AddScoped<IEmailService, EmailService>();

			return services;
		}
	}
}
