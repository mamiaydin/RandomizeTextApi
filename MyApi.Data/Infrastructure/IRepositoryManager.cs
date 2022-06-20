using MyApi.Data.Repositories;

namespace MyApi.Data.Infrastructure
{
    public interface IRepositoryManager
    {
        IRequestsLogRepository RequestsLog { get; }
        void Save();
    }
}