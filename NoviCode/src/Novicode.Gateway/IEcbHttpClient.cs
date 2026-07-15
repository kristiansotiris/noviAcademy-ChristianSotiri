using NoviCode.Gateway.Dtos;

namespace NoviCode.Gateway
{
    public interface IEcbHttpClient
    {
        Task<EcbRatesResult> GetLatestRatesAsync(CancellationToken ct = default);
    }
}
