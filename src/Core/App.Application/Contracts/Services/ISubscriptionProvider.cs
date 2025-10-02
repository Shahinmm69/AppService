namespace App.Application.Contracts.Services
{
    public interface ISubscriptionProvider
    {
        Task<Subscription?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<Subscription>> GetAllAsync(byte[] lastRowVersion, CancellationToken cancellationToken);
    }
}
