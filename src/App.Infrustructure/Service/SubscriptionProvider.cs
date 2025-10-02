namespace App.Infrustructure.Service
{
    public class SubscriptionProvider : ISubscriptionProvider
    {
        private readonly IRedisCache _redis;
        private readonly IPackagingHttpClient _packaging;
        private readonly IRedisHealthMonitor _healthMonitor;
        private readonly IAsyncPolicy _retryPolicy;

        public SubscriptionProvider(
            IRedisCache redis,
            IPackagingHttpClient packaging,
            IRedisHealthMonitor healthMonitor)
        {
            _redis = redis;
            _packaging = packaging;
            _healthMonitor = healthMonitor;

            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));
        }

        public async Task<Subscription?> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            if (_healthMonitor.IsAlive)
                return await _retryPolicy.ExecuteAsync(
                    async () => await _redis.GetByUserIdAsync(userId)
                );
            else
                try
                {
                    return await _retryPolicy.ExecuteAsync(
                        async () => await _redis.GetByUserIdAsync(userId)
                    );
                }
                catch
                {
                    return await _packaging.GetByUserIdAsync(userId, cancellationToken);
                }
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync(byte[] lastRowVersion, CancellationToken cancellationToken)
        {
            if (_healthMonitor.IsAlive)
                return await _retryPolicy.ExecuteAsync(_redis.GetAllAsync);
            else
                try
                {
                    return await _retryPolicy.ExecuteAsync(_redis.GetAllAsync);
                }
                catch
                {
                    return await _packaging.GetAllAsync(lastRowVersion, cancellationToken);
                }
        }
    }
}
