using NoviCode;
using NoviCode.Dtos;

namespace Novicode.Gateway
{
    public class EcbHttpClient : IEcbHttpClient
    {
        public Task<IReadOnlyList<CurrencyRateDto>> GetLatestRatesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
