using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using SimpleRepo.Repo;
using Microsoft.Extensions.Configuration;

namespace Option1ServiceLayer.Tests.ServiceIntegrationTests
{
    public abstract class BaseIntegrationTests<T> : IDisposable
    {
        protected ExampleDbContext SimpleDbContext;

        private string NameOfDb = Guid.NewGuid().ToString("N");

        protected T? Sut;

        public BaseIntegrationTests()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            var connection = configuration.GetSection(nameof(AuthDataSettings));
            var connectionString = (string) connection.GetValue(typeof(string), "ConnectionString");
            SimpleDbContext =
                new ExampleDbContext(
                    connectionString.Replace("{TestDbName}", NameOfDb));
            SimpleDbContext.Database.Migrate();
            Sut = (T) Activator.CreateInstance(typeof(T), new Repo<ExampleDbContext>(SimpleDbContext))!;
        }


        public void Dispose()
        {
            SimpleDbContext.Database.EnsureDeleted();
            SimpleDbContext?.Dispose();
        }
    }
}