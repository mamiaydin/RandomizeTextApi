using MyApi.Data.Data;
using MyApi.Data.Entities.Base;
using MyApi.Data.Entities.Models;
using MyApi.Data.Repositories;

namespace MyApi.Web
{
    public class RequestLogRepository : RepositoryBase<RequestsLog>, IRequestsLogRepository
    {
        public RequestLogRepository(ApplicationDbContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}