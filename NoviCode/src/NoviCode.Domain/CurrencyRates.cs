namespace NoviCode
{
    public class CurrencyRates
    {
        public string Currency { get; } = null!;
        public decimal Rate { get; }
        public DateTime Date { get; }
        public CurrencyRates(string currenty, decimal rate, DateTime date)
        {
            Currency = currenty;
            Rate = rate;
            Date = date;
        }
    }
}
