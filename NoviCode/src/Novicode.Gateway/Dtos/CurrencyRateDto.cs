
namespace NoviCode.Gateway.Dtos
{
    public record CurrencyRateDto(string CurrencyCode, decimal Rate);
    public record EcbRatesResult(DateTime Date, IReadOnlyList<CurrencyRateDto> Rates);

}


