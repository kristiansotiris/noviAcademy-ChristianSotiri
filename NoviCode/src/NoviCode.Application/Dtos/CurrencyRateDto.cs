namespace NoviCode.Dtos
{
    public class CurrencyRateDto
    {
        public string Currency { get; private set; } = null!;
        public decimal Rate { get; private set; }
        public DateTime Date { get; private set; }

        public CurrencyRateDto(string currency, decimal rate, DateTime date)
        {
            Currency = currency;
            Rate = rate;
            Date = date;
        }

    }
}
