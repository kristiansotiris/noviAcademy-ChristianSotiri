using Quartz;

namespace NoviCode.Gateway.Jobs
{
    public class FetchEcbRatesJob : IJob
    {
        private readonly IEcbHttpClient _ecbHttpClient;
        private readonly AppDbContext _db;

        public FetchEcbRatesJob(IEcbHttpClient ecbHttp, AppDbContext db)
        {
            _ecbHttpClient = ecbHttp;
            _db = db;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var result = await _ecbHttpClient.GetLatestRatesAsync();

            var entities = result.Rates.Select(r => new CurrencyRates(r.CurrencyCode, r.Rate, result.Date));

            _db.Set<CurrencyRates>().AddRange(entities);
            await _db.SaveChangesAsync(context.CancellationToken);
        }
    }
}
