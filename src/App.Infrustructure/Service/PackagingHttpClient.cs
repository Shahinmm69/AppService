namespace App.Infrustructure.Service
{
    public class PackagingHttpClient : IPackagingHttpClient
    {
        private readonly HttpClient _http;

        public PackagingHttpClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<Subscription?> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _http.GetFromJsonAsync<Subscription>(
                $"api/subscriptions/by-user-id?userId={userId}", cancellationToken);
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync(byte[] lastRowVersion, CancellationToken cancellationToken)
        {
            var base64 = Convert.ToBase64String(lastRowVersion);
            return await _http.GetFromJsonAsync<IEnumerable<Subscription>>(
                $"api/subscriptions/last-updated?lastRowVersion={base64}", cancellationToken
            ) ?? Enumerable.Empty<Subscription>();
        }
    }
}
