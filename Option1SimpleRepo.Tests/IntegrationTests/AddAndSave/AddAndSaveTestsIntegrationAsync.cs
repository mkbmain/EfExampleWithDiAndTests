using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Option1SimpleRepo.Tests.IntegrationTests.AddAndSave
{
    public class AddAndSaveTestsIntegrationAsync : BaseIntegrationTests
    {
        [Fact]
        public async Task Ensure_addAndSave_works()
        {
            var testGuid = Guid.NewGuid().ToString("N");
            var email = $"{testGuid}@test.com";
            await Sut.Add(new User {Name = testGuid, Email = email});

            var item = await SimpleDbContext.Users.FirstAsync(f => f.Email == email);
            item.Name.ShouldBe(testGuid);
        }
    }
}