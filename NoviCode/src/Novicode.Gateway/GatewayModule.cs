using Microsoft.Extensions.DependencyInjection;

namespace NoviCode.Gateway
{
    public static class GatewayModule
    {
        public static IServiceCollection AddGateway(this IServiceCollection services)
        {
            services.AddHttpClient<IEcbHttpClient, EcbHttpClient>(client =>
            {
                client.BaseAddress = new Uri("https://www.ecb.europa.eu/stats/eurofxref/");
            });

            return services;
        }
    }
}
