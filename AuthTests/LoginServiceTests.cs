using AuthApplication.Models;
using AuthApplication;
using AuthDomain.Interfaces;
using AuthDomain.Entities;
using Moq;

public class LoginServiceTests_Mock
{
    private const string Key = "test-key-123456789012345678901234567890";

    [Fact]
    public void Login_WithValidCredentials_ReturnsToken()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetUserByEmail("test@mail.com"))
                .Returns(new User { Email = "test@mail.com", Password = "123456" });

        var service = new LoginService(mockRepo.Object, Key);

        var response = service.Authenticate(new LoginRequest
        {
            Email = "test@mail.com",
            Password = "123456"
        });

        Assert.NotNull(response);
        Assert.False(string.IsNullOrEmpty(response!.Token));
        mockRepo.Verify(r => r.GetUserByEmail("test@mail.com"), Times.Once);
    }

    [Fact]
    public void Login_WithInvalidCredentials_ReturnsNull()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetUserByEmail("wrong@mail.com"))
                .Returns((User?)null);

        var service = new LoginService(mockRepo.Object, Key);

        var response = service.Authenticate(new LoginRequest
        {
            Email = "wrong@mail.com",
            Password = "wrong"
        });

        Assert.Null(response);
        mockRepo.Verify(r => r.GetUserByEmail("wrong@mail.com"), Times.Once);
    }
}
