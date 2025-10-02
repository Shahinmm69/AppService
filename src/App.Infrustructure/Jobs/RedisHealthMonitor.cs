using App.Application.Contracts.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App.Infrustructure.Jobs
{
    public class RedisHealthMonitor : BackgroundService, IRedisHealthMonitor
    {
        private readonly IRedisCache _redis;
        private readonly ILogger<RedisHealthMonitor> _logger;
        private bool _isAlive;

        public RedisHealthMonitor(IRedisCache redis, ILogger<RedisHealthMonitor> logger)
        {
            _redis = redis;
            _logger = logger;
        }

        public bool IsAlive => _isAlive;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _isAlive = await _redis.IsAliveAsync();
                }
                catch (Exception ex)
                {
                    _isAlive = false;
                    _logger.LogWarning(ex, "Failed to check Redis health");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}
