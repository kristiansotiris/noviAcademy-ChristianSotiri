using MediatR;

namespace NoviCode.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        private static readonly TimeSpan Ttl = TimeSpan.FromSeconds(60);
        private readonly ICache _cache;

        public CachingBehavior(ICache cache) => _cache = cache;
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var key = $"{typeof(TRequest).Name}:{request.GetHashCode()}";

            if (_cache.TryGet<TResponse>(key, out var cached) && cached is not null) return cached;

            var response = await next();

            _cache.Set(key, response, Ttl);
            return response;
        }
    }
}