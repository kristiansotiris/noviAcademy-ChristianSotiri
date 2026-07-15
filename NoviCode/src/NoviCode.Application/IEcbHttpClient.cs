using NoviCode.Dtos;

namespace NoviCode
{
    public interface IEcbHttpClient
    {
        Task<IReadOnlyList<CurrencyRateDto>> GetLatestRatesAsync(CancellationToken cancellationToken = default);
    }
}
