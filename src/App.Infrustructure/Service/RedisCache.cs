namespace Package.Infrustructure.Service
{
    public class RedisCache : IRedisCache
    {
        private readonly IDatabase _db;

        public RedisCache(string connection)
        {
            var conn = ConnectionMultiplexer.Connect(connection);
            _db = conn.GetDatabase();
        }

        public async Task<Subscription?> GetByUserIdAsync(int userId)
        {
            var value = await _db.StringGetAsync($"subscription:{userId}");
            return value.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<Subscription>(value!);
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            var endpoints = _db.Multiplexer.GetEndPoints();
            var server = _db.Multiplexer.GetServer(endpoints.First());

            var keys = server.Keys(pattern: "subscription:*").ToArray();
            var subscription = new List<Subscription>();

            foreach (var key in keys)
            {
                var value = await _db.StringGetAsync(key);
                if (!value.IsNullOrEmpty)
                    subscription.Add(JsonConvert.DeserializeObject<Subscription>(value!)!);
            }

            return subscription;
        }

        public async Task<bool> IsAliveAsync()
        {
            return await _db.KeyExistsAsync("packaging:heartbeat");
        }
    }
}
