using AuthDomain.Entities;

namespace AuthDomain.Interfaces;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
}
