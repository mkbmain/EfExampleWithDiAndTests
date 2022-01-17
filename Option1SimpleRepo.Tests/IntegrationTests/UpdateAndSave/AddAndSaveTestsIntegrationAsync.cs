using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace Option1SimpleRepo.Tests.IntegrationTests.UpdateAndSave
{
    public class UpdateAndSaveTestsIntegrationAsync : BaseIntegrationTests
    {
        [Fact]
        public async Task Ensure_addAndSave_works()
        {
            var testGuid = Guid.NewGuid().ToString("N");
            var email = $"{testGuid}@test.com";
            var user = new User {Name = testGuid, Email = email};
            SimpleDbContext.Users.Add(user);
            await SimpleDbContext.SaveChangesAsync();


            user.Email = "newEmail";
            await Sut.Update(user);

            var item = await SimpleDbContext.Users.FirstAsync(f => f.Id == user.Id);
            item.Email.ShouldBe("newEmail");
        }
    }
}