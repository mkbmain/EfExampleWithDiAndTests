using System.Linq.Expressions;
using DataLayer;
using SimpleRepo.Repo;

namespace ServiceLayer.Service;

public abstract class BaseService<T> : IBaseService<T> where T : class
{
    protected readonly IRepo<ExampleDbContext> Repo;

    public BaseService(IRepo<ExampleDbContext> repo)
    {
        Repo = repo;
    }
    
}