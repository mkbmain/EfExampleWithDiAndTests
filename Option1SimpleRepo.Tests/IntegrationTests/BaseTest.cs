
using Microsoft.EntityFrameworkCore;
using SimpleRepo.Repo;

namespace Option1SimpleRepo.Tests.IntegrationTests
{
    public abstract class BaseIntegrationTests : IDisposable
    {
        protected SimpleDbContext SimpleDbContext = new SimpleDbContext();
        protected Repo<SimpleDbContext> Sut { get; set; }
        public BaseIntegrationTests()
        {
            SimpleDbContext.Database.Migrate();
            Sut = new Repo<SimpleDbContext>(SimpleDbContext);
        }


        public void Dispose()
        {
            SimpleDbContext.Database.EnsureDeleted();
            SimpleDbContext?.Dispose();
        }
    }
}