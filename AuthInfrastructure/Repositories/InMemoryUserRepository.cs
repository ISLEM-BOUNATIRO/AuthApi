using AuthDomain.Entities;
using AuthDomain.Interfaces;

namespace AuthInfrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User { Email = "test@mail.com", Password = "123456" }
    };

    public User? GetUserByEmail(string email) =>
        _users.FirstOrDefault(u => u.Email == email);
}
