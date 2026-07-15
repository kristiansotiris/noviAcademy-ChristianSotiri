using NoviCode.Gateway.Dtos;
using System.Xml.Serialization;

namespace NoviCode.Gateway
{
    public class EcbHttpClient : IEcbHttpClient
    {
        private readonly HttpClient _httpClient;

        public EcbHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EcbRatesResult> GetLatestRatesAsync(CancellationToken ct = default)
        {
            await using var stream = await _httpClient.GetStreamAsync("eurofxref-daily.xml", ct);

            var serializer = new XmlSerializer(typeof(Envelope));

            var envelope = (Envelope)serializer.Deserialize(stream)!;

            var inner = envelope.Cube.Cube1;

            var rates = inner.Cube
            .Select(c => new CurrencyRateDto(c.currency, c.rate)).ToList();

            return new EcbRatesResult(inner.time, rates);
        }
    }
}
