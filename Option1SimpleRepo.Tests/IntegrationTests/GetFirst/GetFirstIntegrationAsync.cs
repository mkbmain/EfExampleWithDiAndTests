using Shouldly;
using Xunit;

namespace Option1SimpleRepo.Tests.IntegrationTests.GetFirst
{
    public class GetFirstIntegrationAsync : BaseIntegrationTests
    {
        [Fact]
        public async Task Ensure_we_can_GetFirst()
        {
            var name = Guid.NewGuid().ToString("N");
            var email = $"{name}@test.com";
            var item = new User {Email = email, Name = name};
            SimpleDbContext.Users.Add(item);
            await SimpleDbContext.SaveChangesAsync();
            SimpleDbContext.Users.Any(t => t.Email == email).ShouldBe(true);

            var item2 = await Sut.Get<User,User>(f => f.Email == email,t=> t);

            item2.ShouldNotBeNull();
            item2.Email.ShouldBe(item.Email);
            item2.Name.ShouldBe(item.Name);
            item2.Id.ShouldBe(item.Id);
        }
    }
}