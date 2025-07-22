using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

public class LoginControllerFunctionalTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public LoginControllerFunctionalTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostLogin_ReturnsOk_WhenCredentialsAreValid()
    {
        var request = new
        {
            Email = "test@mail.com",
            Password = "123456"
        };

        var response = await _client.PostAsJsonAsync("/api/login", request);

        response.EnsureSuccessStatusCode();
    }
}
