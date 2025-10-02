namespace App.Application.Contracts.Services
{
    public interface IPackagingHttpClient
    {
        Task<IEnumerable<Subscription>> GetAllAsync(byte[] lastRowVersion, CancellationToken cancellationToken);
        Task<Subscription?> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
    }
}