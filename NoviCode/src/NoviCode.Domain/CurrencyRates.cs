<<<<<<< HEAD
﻿
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


=======
﻿namespace NoviCode
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
>>>>>>> 4998a994585b665c2b90b4abd81f86c2741ff245
    }
}
