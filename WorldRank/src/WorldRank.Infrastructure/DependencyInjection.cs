using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorldRank.Infrastructure.Contexts;
using WorldRank.Infrastructure.Repositories;
using WorldRank.src.WorldRank.Application.Interfaces;
namespace WorldRank.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		// In-memory repositories hold state, so they must live for the whole app (Singleton).
		//services.AddSingleton<IPlayerRepository, InMemoryPlayerRepository>();
		//services.AddSingleton<IWalletRepository, InMemoryWalletRepository>();


		services.AddDbContext<WorldRankDbContext>(options =>
			options.UseSqlServer()); // sql server 

		services.AddScoped<IPlayerRepository, DBPlayerRepository>();
		services.AddScoped<IWalletRepository, DBWalletRepository>();

		return services;
	}
}
