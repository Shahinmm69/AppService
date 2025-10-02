namespace App.Application.Contracts.Services
{
    public interface IRedisHealthMonitor
    {
        bool IsAlive { get; }
    }
}