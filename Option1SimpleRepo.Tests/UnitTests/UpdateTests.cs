using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Option1SimpleRepo.Tests.UnitTests;

public class UpdateTests
{    [Fact]
    public async Task Ensure_we_can_Update()
    {
        var user = new User{Email = "whatEver"};
        var (sut, mockContext) = RepoFactory.Get();
        var users = new List<User>().AsQueryable().BuildMockDbSet();

        mockContext.Setup(t => t.Set<User>()).Returns(users.Object);
        await sut.Update(user);
        
        // mocks we verify :) 
        users.Verify(t=> t.Update(user),Times.Once);
        mockContext.Verify(t => t.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
}