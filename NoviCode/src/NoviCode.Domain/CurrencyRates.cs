
namespace NoviCode
{
    public class CurrencyRates
    {
        public string CurrencyCode { get; private set; } = null!;
        public decimal Rate { get; private set; }
        public DateTime Date { get; private set; }

        private CurrencyRates(){ }

        public CurrencyRates(string ccode, decimal rate, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(ccode)) throw new ArgumentException("Currency is required", nameof(ccode));
            if (rate < 0) throw new ArgumentException("Rate must be positive", nameof(rate));

            CurrencyCode = ccode;
            Rate = rate;
            Date = date;
        }


    }
}
