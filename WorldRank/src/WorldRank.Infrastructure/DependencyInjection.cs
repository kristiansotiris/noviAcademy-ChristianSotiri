using Microsoft.Extensions.DependencyInjection;
using WorldRank.src.WorldRank.Application.Interfaces;
using WorldRank.src.WorldRank.Infrastructure.Repositories;

namespace WorldRank.src.WorldRank.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		// In-memory repositories hold state, so they must live for the whole app (Singleton).
		services.AddSingleton<IPlayerRepository, InMemoryPlayerRepository>();
		services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();

		return services;
	}
}
