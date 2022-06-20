using MyApi.Data.Data;
using MyApi.Data.Repositories;
using MyApi.Web;

namespace MyApi.Data.Infrastructure
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ApplicationDbContext _dbContext;
        private IRequestsLogRepository _requestsLogRepository;
   
        public RepositoryManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRequestsLogRepository RequestsLog => _requestsLogRepository ??= new RequestLogRepository(_dbContext);

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}