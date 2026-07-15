using NoviCode;
using NoviCode.Dtos;
using System.Xml.Serialization;

namespace Novicode.Gateway
{
    public class EcbHttpClient : IEcbHttpClient
    {
        private readonly HttpClient _httpClient;

        public EcbHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<CurrencyRateDto>> GetLatestRatesAsync(CancellationToken cancellationToken = default)
        {
           var response = await _httpClient.GetAsync("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");

           var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

           var serializer = new XmlSerializer(typeof(CurrencyRateDto));

           var responseDto = serializer.Deserialize(stream);

        }
    }
}
