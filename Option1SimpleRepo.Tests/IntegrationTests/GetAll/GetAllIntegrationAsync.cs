using Shouldly;
using Xunit;

namespace Option1SimpleRepo.Tests.IntegrationTests.GetAll
{
    public class GetAllIntegrationAsync : BaseIntegrationTests
    {
        private List<User> WhatWeShouldHave()
        {
            var users = Enumerable.Range(1, 10).Select(f => Guid.NewGuid().ToString("N"))
                .Select(f => new User
                {
                    Name = f, Email = $"{f}@gmail.com",
                    Posts = new List<Post>() {new Post {CreatedAt = DateTime.Now, Text = f + " Text"}}
                }).ToList();

            SimpleDbContext.Users.AddRange(users);
            SimpleDbContext.SaveChanges();
            return users;
        }

        [Fact]
        public async Task Ensure_we_can_GetAll()
        {
            var users = WhatWeShouldHave();
            var all = await Sut.GetAll<User>();

            users.All(f => all.Select(t => t.Email).Contains(f.Email)).ShouldBeTrue();
        }

        [Fact]
        public async Task Ensure_we_can_GetAll_with_where()
        {
            var users = WhatWeShouldHave();
            var all = await Sut.GetAll<User>(f => f.Email == users.First().Email);

            all.Count().ShouldBe(1);
            all.First().ShouldBe(users.First());
        }

        [Fact]
        public async Task Ensure_we_can_GetAll_with_where_and_projection()
        {
            var users = WhatWeShouldHave();
            var all = await Sut.GetAll<User, string>(f => f.Email == users.First().Email, f => f.Email);

            all.Count().ShouldBe(1);
            all.First().ShouldBe(users.First().Email);
        }
        
        
    }
}