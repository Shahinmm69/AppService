namespace Package.Application.Contracts.Services
{
    public interface IRedisCache
    {
        Task<Subscription?> GetByUserIdAsync(int userId);
        Task<IEnumerable<Subscription>> GetAllAsync();
        Task<bool> IsAliveAsync();
    }
}